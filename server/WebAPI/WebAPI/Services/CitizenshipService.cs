using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;
using WebAPI.Utilities;
using _128Utility.Network.Machine;
using Microsoft.AspNetCore.Http;
using WebAPI.Dtos;
using WebAPI.Dtos.CitizenshipDto;

namespace WebAPI.Services
{
    public class CitizenshipService : ICitizenshipService
    {
        private const string TABLE_NAME = "tbl_fsCitizenship";
        private readonly ICitizenshipRepository repo;
        private readonly IAuditTrailRepository auditTrailRepo;
        private readonly ICompanyInformationRepository comInfoRepo;
        private readonly IFileSetupService fileSetupService;
        private readonly IUserRepository userRepo;
        private readonly IAuditTrailService<Citizenship> auditTrailService;
        public CitizenshipService(ICitizenshipRepository repo,
            IAuditTrailRepository auditTrailRepo,
            IAuditTrailService<Citizenship> auditTrailService,
            ICompanyInformationRepository comInfoRepo,
            IUserRepository userRepo,
            IFileSetupService fileSetupService)
        {
            this.repo = repo;
            this.auditTrailRepo = auditTrailRepo;
            this.auditTrailService = auditTrailService;
            this.comInfoRepo = comInfoRepo;
            this.userRepo = userRepo;
            this.fileSetupService = fileSetupService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var cit = await repo.GetById(id);
            if (cit != null)
            {
                var companyInfo = await comInfoRepo.GetByCompanyCode(comInfoRepo.GetCompanyCode());
                var validationResult =
                    fileSetupService.Validate(companyInfo, await comInfoRepo.CheckDBIfExists(companyInfo.PayrollDB),
                        await comInfoRepo.CheckDBIfExists(companyInfo.TKSDB), await comInfoRepo.CheckDBIfExists(companyInfo.HRISDB));

                if (!validationResult.hasError)
                {
                    return CustomMessageHandler.Error(validationResult.message);
                }
                // Payroll
                var result = await comInfoRepo.CheckTableIfExists(companyInfo.PayrollDB, TABLE_NAME);
                if (result && companyInfo.PayrollFlag)
                {
                    // Check from payroll database
                    var results = await repo.GetByCodeFromPayroll(cit.CitiCode, companyInfo.PayrollDB);
                    if (results == null)
                    {
                        // DELETE FROM PAYROLL DB
                        await repo.DeleteFromHRISFileSetUp(new DeleteSetUpDto
                        {
                            Code = cit.CitiCode,
                            DBName = companyInfo.PayrollDB
                        });
                    }
                }

                // HRIS
                var hrisResult = await comInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                if (hrisResult && companyInfo.HRISFlag)
                {
                    var results = await repo.GetByCodeFromHRIS(cit.CitiCode, companyInfo.HRISDB);
                    if (results == null)
                    {
                        // SAVE TO HRIS DB
                        await repo.DeleteFromPayrollFileSetUp(new DeleteSetUpDto
                        {
                            Code = cit.CitiCode,
                            DBName = companyInfo.HRISDB
                        });
                    }
                }

                await repo.DeleteFileSetUp(cit.CitiCode);
                await repo.Delete(cit.CitiCode);
                

                await auditTrailService.Save(new Citizenship(), cit, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> DeleteByCode(string code)
        {
            var cit = await repo.GetByCode(code);
            if (cit != null)
            {
                var companyInfo = await comInfoRepo.GetByCompanyCode(comInfoRepo.GetCompanyCode());
                var validationResult =
                    fileSetupService.Validate(companyInfo, await comInfoRepo.CheckDBIfExists(companyInfo.PayrollDB),
                        await comInfoRepo.CheckDBIfExists(companyInfo.TKSDB), await comInfoRepo.CheckDBIfExists(companyInfo.HRISDB));

                if (!validationResult.hasError)
                {
                    return CustomMessageHandler.Error(validationResult.message);
                }
                // Payroll
                var result = await comInfoRepo.CheckTableIfExists(companyInfo.PayrollDB, TABLE_NAME);
                if (result && companyInfo.PayrollFlag)
                {
                    // Check from payroll database
                    var results = await repo.GetByCodeFromPayroll(cit.CitiCode, companyInfo.PayrollDB);
                    if (results != null)
                    {
                        // DELETE FROM PAYROLL DB
                        await repo.DeleteFromPayrollFileSetUp(new DeleteSetUpDto
                        {
                            Code = cit.CitiCode,
                            DBName = companyInfo.PayrollDB
                        });
                    }
                }

                // HRIS
                var hrisResult = await comInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                if (hrisResult && companyInfo.HRISFlag)
                {
                    var results = await repo.GetByCodeFromHRIS(cit.CitiCode, companyInfo.HRISDB);
                    if (results != null)
                    {
                        // SAVE TO HRIS DB
                        await repo.DeleteFromHRISFileSetUp(new DeleteSetUpDto
                        {
                            Code = cit.CitiCode,
                            DBName = companyInfo.HRISDB
                        });
                    }
                }

                await repo.DeleteFileSetUp(cit.CitiCode);
                await repo.Delete(cit.CitiCode);


                await auditTrailService.Save(new Citizenship(), cit, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Citizenship cit)
        {
            // Get Company Code
            cit.CompanyCode = comInfoRepo.GetCompanyCode();
            cit.CreatedBy = userRepo.GetEmpCode();
            if (String.IsNullOrEmpty(cit.CitiCode) || String.IsNullOrWhiteSpace(cit.CitiCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            var citData = await repo.GetByCode(cit.CitiCode);

            if (citData == null)
            {
                var companyInfo = await comInfoRepo.GetByCompanyCode(comInfoRepo.GetCompanyCode());
                var validationResult =
                    fileSetupService.Validate(companyInfo, await comInfoRepo.CheckDBIfExists(companyInfo.PayrollDB),
                        await comInfoRepo.CheckDBIfExists(companyInfo.TKSDB), await comInfoRepo.CheckDBIfExists(companyInfo.HRISDB));

                if (!validationResult.hasError)
                {
                    return CustomMessageHandler.Error(validationResult.message);
                }
                else
                {
                    // PAYROLL 
                    var result = await comInfoRepo.CheckTableIfExists(companyInfo.PayrollDB, TABLE_NAME);
                    if (result && companyInfo.PayrollFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromPayroll(cit.CitiCode, companyInfo.PayrollDB);
                        if(results == null)
                        {
                            // SAVE TO PAYROLL DB
                            await repo.InsertToPayrollFileSetUp(new CitizenshipInsertToFileSetUpDto
                            {
                                CitiCode = cit.CitiCode,
                                CitiDesc = cit.CitiDesc,
                                DBName = companyInfo.PayrollDB,
                                CreatedBy = cit.CreatedBy // Current User
                            });
                        }
                        
                    }
                    // HRIS
                    var hrisResult = await comInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        var results = await repo.GetByCodeFromHRIS(cit.CitiCode, companyInfo.HRISDB);
                        if(results == null)
                        {
                            // SAVE TO HRIS DB
                            await repo.InsertToHRISFileSetUp(new CitizenshipInsertToFileSetUpDto
                            {
                                CitiCode = cit.CitiCode,
                                CitiDesc = cit.CitiDesc,
                                DBName = companyInfo.HRISDB,
                                CreatedBy = cit.CreatedBy // Current User
                            });
                        }
                        
                    }
                }

                await repo.InsertFileSetup(cit);
                await repo.Insert(cit);
                await auditTrailService.Save(new Citizenship(), cit, "ADD");

                return CustomMessageHandler.RecordAdded();
               
            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Citizenship cit)
        {
            var citData = await repo.GetByCode(cit.CitiCode);
            cit.EditedBy = userRepo.GetEmpCode();
            if (citData != null)
            {
                var companyInfo = await comInfoRepo.GetByCompanyCode(comInfoRepo.GetCompanyCode());
                var validationResult =
                    fileSetupService.Validate(companyInfo, await comInfoRepo.CheckDBIfExists(companyInfo.PayrollDB),
                        await comInfoRepo.CheckDBIfExists(companyInfo.TKSDB), await comInfoRepo.CheckDBIfExists(companyInfo.HRISDB));

                if (!validationResult.hasError)
                {
                    return CustomMessageHandler.Error(validationResult.message);
                }
                else
                {
                    // PAYROLL 
                    var result = await comInfoRepo.CheckTableIfExists(companyInfo.PayrollDB, TABLE_NAME);
                    if (result && companyInfo.PayrollFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromPayroll(cit.CitiCode, companyInfo.PayrollDB);
                        if (results != null)
                        {
                            // SAVE TO PAYROLL DB
                            await repo.UpdateToPayrollFileSetUp(new CitizenshipUpdateToFileSetUpDto
                            {
                                CitiCode = cit.CitiCode,
                                CitiDesc = cit.CitiDesc,
                                DBName = companyInfo.PayrollDB,
                                EditedBy = cit.EditedBy // Current User
                            });
                        }
                    }

                    // HRIS 
                    var hrisResult = await comInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromHRIS(cit.CitiCode, companyInfo.HRISDB);
                        if (results != null)
                        {
                            // SAVE TO PAYROLL DB
                            await repo.UpdateToHRISFileSetUp(new CitizenshipUpdateToFileSetUpDto
                            {
                                CitiCode = cit.CitiCode,
                                CitiDesc = cit.CitiDesc,
                                DBName = companyInfo.HRISDB,
                                EditedBy = cit.EditedBy // Current User
                            });
                        }
                    }


                    await repo.UpdateFileSetup(cit);
                    await repo.Update(cit);

                    //Audit Trail
                    await auditTrailService.Save(citData, cit, "EDIT");

                    return CustomMessageHandler.RecordUpdated();
                }
            }
            return CustomMessageHandler.Error("You can't edit this code");
        }

    }
}
