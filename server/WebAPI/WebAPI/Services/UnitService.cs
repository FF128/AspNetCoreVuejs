using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.Unit;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class UnitService : IUnitService
    {
        private const string TABLE_NAME = "tbl_fsUnit";
        private readonly IUnitRepository repo;
        private readonly IAuditTrailService<Unit> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IFileSetupService fileSetupService;
        public UnitService(IUnitRepository repo,
             IAuditTrailService<Unit> auditTrailService,
             ICompanyInformationRepository compInfoRepo,
             IFileSetupService fileSetupService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
            this.fileSetupService = fileSetupService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var unit = await repo.GetById(id);
            if (unit != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Unit(), unit, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Delete(string code)
        {
            var unit = await repo.GetByCode(code);
            if (unit != null)
            {
                await repo.DeleteByCode(code);

                await auditTrailService.Save(new Unit(), unit, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Unit unit)
        {
            // Get Company Code
            unit.CompanyCode = compInfoRepo.GetCompanyCode();
            if (String.IsNullOrEmpty(unit.UnitCode) || String.IsNullOrWhiteSpace(unit.UnitCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            var unitData = await repo.GetByCode(unit.UnitCode);

            if (unitData == null)
            {
                var companyInfo = await compInfoRepo.GetByCompanyCode(compInfoRepo.GetCompanyCode());
                var validationResult =
                    fileSetupService.Validate(companyInfo, await compInfoRepo.CheckDBIfExists(companyInfo.PayrollDB),
                        await compInfoRepo.CheckDBIfExists(companyInfo.TKSDB), await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB));

                if (!validationResult.hasError)
                {
                    return CustomMessageHandler.Error(validationResult.message);
                }
                else
                {
                    // PAYROLL 
                    var result = await compInfoRepo.CheckTableIfExists(companyInfo.PayrollDB, TABLE_NAME);
                    if (result && companyInfo.PayrollFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromPayroll(unit.UnitCode, companyInfo.PayrollDB);
                        if (results == null)
                        {
                            // SAVE TO PAYROLL DB
                            await repo.InsertToPayrollFileSetUp(new UnitInsertToPayrollFSDto
                            {
                                UnitCode = unit.UnitCode,
                                UnitDesc = unit.UnitDesc,
                                AccountCode = unit.AcctCode,
                                Head = unit.Head,
                                HeadCode = unit.HeadCode,
                                CreatedBy = "", //Current User
                                DBName = companyInfo.PayrollDB
                            });
                        }

                    }
                    
                    // HRIS
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        var results = await repo.GetByCodeFromHRIS(unit.UnitCode, companyInfo.HRISDB);
                        if (results == null)
                        {
                            // SAVE TO HRIS DB
                            await repo.InsertToHRISFileSetUp(new UnitInsertToHRISFSDto
                            {
                                UnitCode = unit.UnitCode,
                                UnitDesc = unit.UnitDesc,
                                AccountCode = unit.AcctCode,
                                Head = unit.Head,
                                HeadCode = unit.HeadCode,
                                DBName = companyInfo.HRISDB
                            });
                        }

                    }
                }


                await repo.InsertFileSetup(unit);
                await repo.Insert(unit);
                await auditTrailService.Save(new Unit(), unit, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Unit unit)
        {
            var unitData = await repo.GetByCode(unit.UnitCode);
            if (unitData != null)
            {
                var companyInfo = await compInfoRepo.GetByCompanyCode(compInfoRepo.GetCompanyCode());
                var validationResult =
                    fileSetupService.Validate(companyInfo, await compInfoRepo.CheckDBIfExists(companyInfo.PayrollDB),
                        await compInfoRepo.CheckDBIfExists(companyInfo.TKSDB), await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB));

                if (!validationResult.hasError)
                {
                    return CustomMessageHandler.Error(validationResult.message);
                }
                else
                {

                    // PAYROLL 
                    var result = await compInfoRepo.CheckTableIfExists(companyInfo.PayrollDB, TABLE_NAME);
                    if (result && companyInfo.PayrollFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromPayroll(unit.UnitCode, companyInfo.PayrollDB);
                        if (results != null)
                        {
                            // SAVE TO PAYROLL DB
                            await repo.UpdateToPayrollFileSetUp(new UnitUpdateToPayrollFSDto
                            {
                                UnitCode = unit.UnitCode,
                                UnitDesc = unit.UnitDesc,
                                AccountCode = unit.AcctCode,
                                Head = unit.Head,
                                HeadCode = unit.HeadCode,
                                CreatedBy = "", //Current User
                                DBName = companyInfo.PayrollDB
                            });
                        }
                    }

                    // HRIS 
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromHRIS(unit.UnitCode, companyInfo.HRISDB);
                        if (results != null)
                        {
                            // SAVE TO PAYROLL DB
                            await repo.UpdateToHRISFileSetUp(new UnitUpdateToHRISFSDto
                            {
                                UnitCode = unit.UnitCode,
                                UnitDesc = unit.UnitDesc,
                                AccountCode = unit.AcctCode,
                                Head = unit.Head,
                                HeadCode = unit.HeadCode,
                                DBName = companyInfo.HRISDB
                            });
                        }
                    }

                    await repo.UpdateFileSetup(unit);
                    await repo.Update(unit);

                    //Audit Trail
                    await auditTrailService.Save(unitData, unit, "EDIT");

                    return CustomMessageHandler.RecordUpdated();
                }
            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
