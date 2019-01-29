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

namespace WebAPI.Services
{
    public class CitizenshipService : ICitizenshipService
    {
        private const string TABLE_NAME = "tbl_fsCitizenship";
        private readonly ICitizenshipRepository repo;
        private readonly IAuditTrailRepository auditTrailRepo;
        private readonly ICompanyInformationRepository comInfoRepo;

        private readonly IAuditTrailService<Citizenship> auditTrailService;
        public CitizenshipService(ICitizenshipRepository repo,
            IAuditTrailRepository auditTrailRepo,
            IAuditTrailService<Citizenship> auditTrailService,
            ICompanyInformationRepository comInfoRepo)
        {
            this.repo = repo;
            this.auditTrailRepo = auditTrailRepo;
            this.auditTrailService = auditTrailService;
            this.comInfoRepo = comInfoRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var cit = await repo.GetById(id);
            if (cit != null)
            {
                await repo.Delete(id);

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
                await repo.DeleteByCode(code);

                await auditTrailService.Save(new Citizenship(), cit, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Citizenship cit)
        {
            // Get Company Code
            cit.CompanyCode = comInfoRepo.GetCompanyCode();
            if (String.IsNullOrEmpty(cit.CitiCode) || String.IsNullOrWhiteSpace(cit.CitiCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            var citData = await repo.GetByCode(cit.CitiCode);

            if (citData == null)
            {
                var companyInfo = await comInfoRepo.GetByCompanyCode(comInfoRepo.GetCompanyCode());
                if(companyInfo == null)
                {
                    return CustomMessageHandler.Error("Company Information doesn't exist");
                }

                if(!(await comInfoRepo.CheckDBIfExists(companyInfo.PayrollDB)))
                {
                    return CustomMessageHandler.Error("Payroll Database doesn't exist!");
                }
                else if(!(await comInfoRepo.CheckDBIfExists(companyInfo.TKSDB)))
                {
                    return CustomMessageHandler.Error("Timekeeping Database doesn't exist!");
                }
                else if(!(await comInfoRepo.CheckDBIfExists(companyInfo.HRISDB)))
                {
                    return CustomMessageHandler.Error("HRIS Database doesn't exist!");
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
                                CreatedBy = "" // Current User
                            });
                        }
                        
                    }

                    // TIME KEEPING
                    var tksResult = await comInfoRepo.CheckTableIfExists(companyInfo.TKSDB, TABLE_NAME);
                    if (tksResult && companyInfo.TKSFlag)
                    {
                        //var results = await repo.GetByCodeFromPayroll(cit.CitiCode, companyInfo.PayrollDB);

                        //// SAVE TO TKS DB
                        //await repo.InsertToPayrollFileSetUp(new CitizenshipInsertToFileSetUpDto
                        //{
                        //    CitiCode = cit.CitiCode,
                        //    CitiDesc = cit.CitiDesc,
                        //    DBName = companyInfo.TKSDB,
                        //    CreatedBy = "" // Current User
                        //});
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
                                CreatedBy = "" // Current User
                            });
                        }
                        
                    }
                }
                #region Condition for saving citizenship file setup
                //if(companyInfo.PayrollFlag) // Check if Payroll is integrated
                //{
                //    #region Check if Payroll Database exists

                //    if (await comInfoRepo.CheckDBIfExists(companyInfo.PayrollDB)) 
                //    {

                //        var result = await comInfoRepo.CheckTableIfExists(companyInfo.PayrollDB, "tbl_fsCitizenship");
                //        if(result)
                //        {
                //            //save
                //        }
                //    }
                //    else
                //    {
                //        return CustomMessageHandler.Error("Database doesn't exist!");
                //    }
                //    #endregion
                //}
                //if (companyInfo.TKSFlag) // Check if Timekeeping is integrated
                //{
                //    #region Check if TKS Database exists

                //    if (await comInfoRepo.CheckDBIfExists(companyInfo.TKSDB))
                //    {
                //        var result = await comInfoRepo.CheckTableIfExists(companyInfo.TKSDB, "tbl_fsCitizenship");
                //        if (result)
                //        {
                //            // Save
                //        }
                //    }
                //    else
                //    {
                //        return CustomMessageHandler.Error("Database doesn't exist!");

                //    }
                //    #endregion
                //}
                //if (companyInfo.EmpOnlineFlag) // Check if Employee Online is integrated
                //{
                //    #region Check if Employee Online Database exists

                //    if (await comInfoRepo.CheckDBIfExists(companyInfo.OnlineDB))
                //    {

                //    }
                //    else
                //    {
                //        return CustomMessageHandler.Error("Database doesn't exist!");

                //    }
                //    #endregion
                //}
                //if (companyInfo.HRISFlag) // Check if HRIS is integrated
                //{
                //    #region Check if HRIS Database exists

                //    if (await comInfoRepo.CheckDBIfExists(companyInfo.HRISDB))
                //    {

                //    }
                //    else
                //    {
                //        return CustomMessageHandler.Error("Database doesn't exist!");

                //    }
                //    #endregion
                //}
                #endregion

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
            if (citData != null)
            {
                await repo.Update(cit);

                //Audit Trail
                await auditTrailService.Save(citData, cit, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }

    }
}
