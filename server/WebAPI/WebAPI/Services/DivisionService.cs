using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Dtos.Division;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class DivisionService : IDivisionService
    {
        private const string TABLE_NAME = "tbl_fsDivision";
        private readonly IDivisionRepository repo;
        private readonly IAuditTrailService<Division> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IFileSetupService fileSetupService;
        public DivisionService(IDivisionRepository repo,
             IAuditTrailService<Division> auditTrailService,
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
            var division = await repo.GetById(id);
            if (division != null)
            {
                //await repo.Delete(id);

                await auditTrailService.Save(new Division(), division, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> DeleteByCode(string code)
        {
            var div = await repo.GetByCode(code);
            if (div != null)
            {
                var companyInfo = await compInfoRepo.GetByCompanyCode(compInfoRepo.GetCompanyCode());
                var validationResult =
                    fileSetupService.Validate(companyInfo, await compInfoRepo.CheckDBIfExists(companyInfo.PayrollDB),
                        await compInfoRepo.CheckDBIfExists(companyInfo.TKSDB), await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB));

                if (!validationResult.hasError)
                {
                    return CustomMessageHandler.Error(validationResult.message);
                }
                // Payroll
                var result = await compInfoRepo.CheckTableIfExists(companyInfo.PayrollDB, TABLE_NAME);
                if (result && companyInfo.PayrollFlag)
                {
                    // Check from payroll database
                    var results = await repo.GetByCodeFromPayroll(div.DivisionCode, companyInfo.PayrollDB);
                    if (results != null)
                    {
                        // DELETE FROM PAYROLL DB
                        await repo.DeleteFromPayrollFileSetUp(new DeleteSetUpDto
                        {
                            Code = div.DivisionCode,
                            DBName = companyInfo.PayrollDB
                        });
                    }
                }
                // TIME KEEPING
                var tksResult = await compInfoRepo.CheckTableIfExists(companyInfo.TKSDB, TABLE_NAME);
                if (tksResult && companyInfo.TKSFlag)
                {
                    var results = await repo.GetByCodeFromTKS(div.DivisionCode, companyInfo.TKSDB);
                    if (results != null)
                    {
                        // SAVE TO TKS DB
                        await repo.DeleteFromTKSFileSetUp(new DeleteSetUpDto
                        {
                            Code = div.DivisionCode,
                            DBName = companyInfo.TKSDB
                        });
                    }

                }
                // HRIS
                var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                if (hrisResult && companyInfo.HRISFlag)
                {
                    var results = await repo.GetByCodeFromHRIS(div.DivisionCode, companyInfo.HRISDB);
                    if (results != null)
                    {
                        // SAVE TO HRIS DB
                        await repo.DeleteFromHRISFileSetUp(new DeleteSetUpDto
                        {
                            Code = div.DivisionCode,
                            DBName = companyInfo.HRISDB
                        });
                    }
                }

                await repo.DeleteFileSetUp(div.DivisionCode);
                await repo.Delete(div.DivisionCode);


                await auditTrailService.Save(new Division(), div, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Division div)
        {
            // Get Company Code
            div.CompanyCode = compInfoRepo.GetCompanyCode();
            if (String.IsNullOrEmpty(div.DivisionCode) || String.IsNullOrWhiteSpace(div.DivisionCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            var divData = await repo.GetByCode(div.DivisionCode);

            if (divData == null)
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
                    if (result && companyInfo.TKSFlag)
                    {
                        var results = await repo.GetByCodeFromPayroll(div.DivisionCode, companyInfo.PayrollDB);
                        if (results == null)
                        {
                            // SAVE TO TKS DB
                            await repo.InsertToPayrollFileSetUp(new DivInsertToPayrollFSDto
                            {
                                DivCode = div.DivisionCode,
                                DivDesc = div.DivisionDesc,
                                DivHead = div.Head,
                                AccountCode = div.AcctCode,
                                CreatedBy = "", //Current User
                                DBName = companyInfo.PayrollDB
                            });
                        }

                    }

                    // TIME KEEPING
                    var tksResult = await compInfoRepo.CheckTableIfExists(companyInfo.TKSDB, TABLE_NAME);
                    if (tksResult && companyInfo.TKSFlag)
                    {
                        var results = await repo.GetByCodeFromTKS(div.DivisionCode, companyInfo.TKSDB);
                        if (results == null)
                        {
                            // SAVE TO TKS DB
                            await repo.InsertToTKSFileSetUp(new DivInsertToTKSFSDto
                            {
                                DivCode = div.DivisionCode,
                                DivDesc = div.DivisionDesc,
                                DivHead = div.Head,
                                DivHeadCode = div.HeadCode,
                                DBName = companyInfo.TKSDB
                            });
                        }

                    }
                    // HRIS
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        var results = await repo.GetByCodeFromHRIS(div.DivisionCode, companyInfo.HRISDB);
                        if (results == null)
                        {
                            // SAVE TO HRIS DB
                            await repo.InsertToHRISFileSetUp(new DivInsertToHRISFSDto
                            {
                                DivCode = div.DivisionCode,
                                DivDesc = div.DivisionDesc,
                                DivHead = div.Head,
                                DivHeadCode = div.HeadCode,
                                DBName = companyInfo.HRISDB,
                                CreatedBy = "" // Current User
                            });
                        }

                    }
                }


                await repo.InsertFileSetup(new DivInsertFileSetupDto
                {
                    DivCode = div.DivisionCode,
                    DivDesc = div.DivisionDesc,
                    Head = div.Head,
                    HeadCode = div.HeadCode,
                    CreatedBy = "" // Current User
                });
                await repo.Insert(div);
                await auditTrailService.Save(new Division(), div, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Division div)
        {
            var divData = await repo.GetByCode(div.DivisionCode);
            if (divData != null)
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
                        var results = await repo.GetByCodeFromPayroll(div.DivisionCode, companyInfo.PayrollDB);
                        if (results != null)
                        {
                            // SAVE TO PAYROLL DB
                            await repo.UpdateToPayrollFileSetUp(new DivUpdateToPayrollFSDto
                            {
                                DivCode = div.DivisionCode,
                                DivDesc = div.DivisionDesc,
                                DivHead = div.Head,
                                AccountCode = div.AcctCode,
                                EditedBy = "", //Current User
                                DBName = companyInfo.PayrollDB
                            });
                        }
                    }
                    // TIME KEEPING
                    var tksResult = await compInfoRepo.CheckTableIfExists(companyInfo.TKSDB, TABLE_NAME);
                    if (tksResult && companyInfo.TKSFlag)
                    {
                        var results = await repo.GetByCodeFromTKS(div.DivisionCode, companyInfo.TKSDB);
                        if (results != null)
                        {
                            // SAVE TO TKS DB
                            await repo.UpdateToTKSFileSetUp(new DivUpdateToTKSFSDto
                            {
                                DivCode = div.DivisionCode,
                                DivDesc = div.DivisionDesc,
                                DivHead = div.Head,
                                DivHeadCode = div.HeadCode,
                                DBName = companyInfo.TKSDB
                            });
                        }
                    }
                    // HRIS 
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromHRIS(div.DivisionCode, companyInfo.HRISDB);
                        if (results != null)
                        {
                            // SAVE TO PAYROLL DB
                            await repo.UpdateToHRISFileSetUp(new DivUpdateToHRISFSDto
                            {
                                DivCode = div.DivisionCode,
                                DivDesc = div.DivisionDesc,
                                DivHead = div.Head,
                                DivHeadCode = div.HeadCode,
                                DBName = companyInfo.HRISDB,
                                EditedBy = "" // Current User
                            });
                        }
                    }

                    await repo.UpdateFileSetup(new DivUpdateFileSetupDto
                    {
                        DivCode = div.DivisionCode,
                        DivDesc = div.DivisionDesc,
                        Head = div.Head,
                        HeadCode = div.HeadCode,
                        EditedBy = "" // Current User
                    });
                    await repo.Update(div);

                    //Audit Trail
                    await auditTrailService.Save(divData, div, "EDIT");

                    return CustomMessageHandler.RecordUpdated();
                }
            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
