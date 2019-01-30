﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.Branch;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class BranchService : IBranchService
    {
        private const string TABLE_NAME = "tbl_fsBranch";
        private readonly IBranchRepository repo;
        private readonly IAuditTrailService<Branch> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IFileSetupService fileSetupService;
        public BranchService(IBranchRepository repo,
             IAuditTrailService<Branch> auditTrailService,
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
            var branch = await repo.GetById(id);
            if (branch != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Branch(), branch, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public Task<CustomMessage> DeleteByCode(string code)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomMessage> Insert(Branch branch)
        {
            branch.CompanyCode = compInfoRepo.GetCompanyCode();
            if (String.IsNullOrEmpty(branch.BranchCode) || String.IsNullOrWhiteSpace(branch.BranchCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            var branchData = await repo.GetByCode(branch.BranchCode);

            if (branchData == null)
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
                    if ((bool)result && companyInfo.PayrollFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromPayroll(branch.BranchCode, companyInfo.PayrollDB);
                        if (results == null)
                        {
                            // SAVE TO PAYROLL DB
                            await repo.InsertToPayrollFileSetUp(new BranchInsertToPayrollFSDto
                            {
                                BraCode = branch.BranchCode,
                                BraDesc = branch.BranchDesc,
                                AccountCode = branch.AcctCode,
                                DBName = companyInfo.PayrollDB,
                                CreatedBy = "" // Current User
                            });
                        }

                    }
                    // (databaseName, tableName, string code)
                    // TIME KEEPING
                    var tksResult = await compInfoRepo.CheckTableIfExists(companyInfo.TKSDB, TABLE_NAME);
                    if ((bool)tksResult && companyInfo.TKSFlag)
                    {
                        var results = await repo.GetByCodeFromTKS(branch.BranchCode, companyInfo.TKSDB);
                        if (results == null)
                        {
                            // SAVE TO TKS DB
                            await repo.InsertToTSKFileSetUp(new BranchInsertToTKSFSDto
                            {
                                BraCode = branch.BranchCode,
                                BraDesc = branch.BranchDesc,
                                DBName = companyInfo.TKSDB
                            });
                        }

                    }
                    // HRIS
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if ((bool)hrisResult && companyInfo.HRISFlag)
                    {
                        var results = await repo.GetByCodeFromHRIS(branch.BranchCode, companyInfo.HRISDB);
                        if (results == null)
                        {
                            // SAVE TO HRIS DB
                            await repo.InsertToHRISFileSetUp(new BranchInsertToHRISFSDto
                            {
                                BraCode = branch.BranchCode,
                                BraDesc = branch.BranchDesc,
                                DBName = companyInfo.HRISDB,
                                CreatedBy = "" // Current User
                            });
                        }

                    }
                }


                await repo.InsertFileSetup(new BranchInsertToFSDto
                {
                    BraCode = branch.BranchCode,
                    BraDesc = branch.BranchDesc,
                    CreatedBy = "" // Current User
                });
                await repo.Insert(branch);
                await auditTrailService.Save(new Branch(), branch, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Branch branch)
        {
            var branchData = await repo.GetByCode(branch.BranchCode);
            if (branchData != null)
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
                        var results = await repo.GetByCodeFromPayroll(branch.BranchCode, companyInfo.PayrollDB);
                        if (results != null)
                        {
                            // SAVE TO PAYROLL DB
                            await repo.UpdateToPayrollFileSetUp(new BranchUpdateToPayrollFSDto
                            {
                                BraCode = branch.BranchCode,
                                BraDesc = branch.BranchDesc,
                                AccountCode = branch.AcctCode,
                                DBName = companyInfo.PayrollDB,
                                EditedBy = "" // Current User
                            });
                        }
                    }
                    // TIME KEEPING
                    var tksResult = await compInfoRepo.CheckTableIfExists(companyInfo.TKSDB, TABLE_NAME);
                    if (tksResult && companyInfo.TKSFlag)
                    {
                        var results = await repo.GetByCodeFromTKS(branch.BranchCode, companyInfo.TKSDB);
                        if (results != null)
                        {
                            // SAVE TO TKS DB
                            await repo.UpdateToTKSFileSetUp(new BranchUpdateToTKSFSDto
                            {
                                BraCode = branch.BranchCode,
                                BraDesc = branch.BranchDesc,
                                DBName = companyInfo.TKSDB
                            });
                        }
                    }
                    // HRIS 
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromHRIS(branch.BranchCode, companyInfo.HRISDB);
                        if (results != null)
                        {
                            // SAVE TO PAYROLL DB
                            await repo.UpdateToHRISFileSetUp(new BranchUpdateToHRISFSDto
                            {
                                BraCode = branch.BranchCode,
                                BraDesc = branch.BranchDesc,
                                DBName = companyInfo.HRISDB,
                                EditedBy = "" // Current User
                            });
                        }
                    }

                    await repo.UpdateFileSetup(new BranchUpdateToFSDto
                    {
                        BraCode = branch.BranchCode,
                        BraDesc = branch.BranchDesc,
                        EditedBy = "" // Current User
                    });
                    await repo.Update(branch);

                    //Audit Trail
                    await auditTrailService.Save(branchData, branch, "EDIT");

                    return CustomMessageHandler.RecordUpdated();
                }
            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
