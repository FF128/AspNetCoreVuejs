using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;
using WebAPI.Dtos.Department;
using WebAPI.Dtos;

namespace WebAPI.Services
{
    public class DepartmentService : IDepartmentService
    {
        private const string TABLE_NAME = "tbl_fsDepartment";
        private readonly IDepartmentRepository repo;
        private readonly IAuditTrailService<Department> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IUserRepository userRepo;
        private readonly IFileSetupService fileSetupService;
        public DepartmentService(IDepartmentRepository repo,
             IAuditTrailService<Department> auditTrailService,
             ICompanyInformationRepository compInfoRepo,
             IUserRepository userRepo,
             IFileSetupService fileSetupService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
            this.userRepo = userRepo;
            this.fileSetupService = fileSetupService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var Department = await repo.GetById(id);
            if (Department != null)
            {
                //await repo.Delete(id);

                await auditTrailService.Save(new Department(), Department, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> DeleteByCode(string code)
        {
            var dep = await repo.GetByCode(code);
            if (dep != null)
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
                    var results = await repo.GetByCodeFromPayroll(dep.DepartmentCode, companyInfo.PayrollDB);
                    if (results != null)
                    {
                        // DELETE FROM PAYROLL DB
                        await repo.DeleteFromPayrollFileSetUp(new DeleteSetUpDto
                        {
                            Code = dep.DepartmentCode,
                            DBName = companyInfo.PayrollDB
                        });
                    }
                }
                // TIME KEEPING
                var tksResult = await compInfoRepo.CheckTableIfExists(companyInfo.TKSDB, TABLE_NAME);
                if (tksResult && companyInfo.TKSFlag)
                {
                    var results = await repo.GetByCodeFromTKS(dep.DepartmentCode, companyInfo.TKSDB);
                    if (results != null)
                    {
                        // SAVE TO TKS DB
                        await repo.DeleteFromTKSFileSetUp(new DeleteSetUpDto
                        {
                            Code = dep.DepartmentCode,
                            DBName = companyInfo.TKSDB
                        });
                    }

                }
                // HRIS
                var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                if (hrisResult && companyInfo.HRISFlag)
                {
                    var results = await repo.GetByCodeFromHRIS(dep.DepartmentCode, companyInfo.HRISDB);
                    if (results != null)
                    {
                        // SAVE TO HRIS DB
                        await repo.DeleteFromHRISFileSetUp(new DeleteSetUpDto
                        {
                            Code = dep.DepartmentCode,
                            DBName = companyInfo.HRISDB
                        });
                    }
                }

                await repo.DeleteFileSetUp(dep.DepartmentCode);
                await repo.Delete(dep.DepartmentCode);


                await auditTrailService.Save(new Department(), dep, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Department dep)
        {
            dep.CompanyCode = compInfoRepo.GetCompanyCode();
            dep.CreatedBy = userRepo.GetEmpCode();
            if (String.IsNullOrEmpty(dep.DepartmentCode) || String.IsNullOrWhiteSpace(dep.DepartmentCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            var depData = await repo.GetByCode(dep.DepartmentCode);

            if (depData == null)
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
                        var results = await repo.GetByCodeFromPayroll(dep.DepartmentCode, companyInfo.PayrollDB);
                        if (results == null)
                        {
                            // SAVE TO PAYROLL DB
                            await repo.InsertToPayrollFileSetUp(new DepartmentInsertToPayrollFSDto
                            {
                                DepCode = dep.DepartmentCode,
                                DepDesc = dep.DepartmentDesc,
                                DBName = companyInfo.PayrollDB,
                                CreatedBy = dep.CreatedBy // Current User
                            });
                        }
                    }
                    // TIME KEEPING
                    var tksResult = await compInfoRepo.CheckTableIfExists(companyInfo.TKSDB, TABLE_NAME);
                    if ((bool)tksResult && companyInfo.TKSFlag)
                    {
                        var results = await repo.GetByCodeFromTKS(dep.DepartmentCode, companyInfo.TKSDB);
                        if (results == null)
                        {
                            // SAVE TO TKS DB
                            await repo.InsertToTKSFileSetUp(new DepartmentInsertToTKSFSDto
                            {
                                DepDesc = dep.DepartmentDesc,
                                DepCode = dep.DepartmentCode,
                                DBName = companyInfo.TKSDB
                            });
                        }

                    }
                    // HRIS
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if ((bool)hrisResult && companyInfo.HRISFlag)
                    {
                        var results = await repo.GetByCodeFromHRIS(dep.DepartmentCode, companyInfo.HRISDB);
                        if (results == null)
                        {
                            // SAVE TO HRIS DB
                            await repo.InsertToHRISFileSetUp(new DepartmentInsertToHRISFSDto
                            {
                                DepCode = dep.DepartmentCode,
                                DepDesc = dep.DepartmentDesc,
                                DBName = companyInfo.HRISDB,
                                CreatedBy = dep.CreatedBy // Current User
                            });
                        }

                    }
                }


                await repo.InsertFileSetup(dep);
                await repo.Insert(dep);
                await auditTrailService.Save(new Department(), dep, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Department dep)
        {
            var depData = await repo.GetByCode(dep.DepartmentCode);
            dep.EditedBy = userRepo.GetEmpCode();
            if (depData != null)
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
                        var results = await repo.GetByCodeFromPayroll(dep.DepartmentCode, companyInfo.PayrollDB);
                        if (results != null)
                        {
                            // SAVE TO PAYROLL DB
                            await repo.UpdateToPayrollFileSetUp(new DepartmentUpdateToPayrollFSDto
                            {
                                DepCode = dep.DepartmentCode,
                                DepDesc = dep.DepartmentDesc,
                                DBName = companyInfo.PayrollDB,
                                EditedBy = dep.EditedBy // Current User
                            });
                        }
                    }
                    // TIME KEEPING
                    var tksResult = await compInfoRepo.CheckTableIfExists(companyInfo.TKSDB, TABLE_NAME);
                    if (tksResult && companyInfo.TKSFlag)
                    {
                        var results = await repo.GetByCodeFromTKS(dep.DepartmentCode, companyInfo.TKSDB);
                        if (results != null)
                        {
                            // SAVE TO TKS DB
                            await repo.UpdateToTKSFileSetUp(new DepartmentUpdateToTKSFSDto
                            {
                                DepDesc = dep.DepartmentDesc,
                                DepCode = dep.DepartmentCode,
                                DBName = companyInfo.TKSDB
                            });
                        }
                    }
                    // HRIS 
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromHRIS(dep.DepartmentCode, companyInfo.HRISDB);
                        if (results != null)
                        {
                            // SAVE TO PAYROLL DB
                            await repo.UpdateToHRISFileSetUp(new DepartmentUpdateToHRISFSDto
                            {
                                DepCode = dep.DepartmentCode,
                                DepDesc = dep.DepartmentDesc,
                                DBName = companyInfo.HRISDB,
                                EditedBy = dep.EditedBy // Current User
                            });
                        }
                    }

                    await repo.UpdateFileSetup(dep);
                    await repo.Update(dep);

                    //Audit Trail
                    await auditTrailService.Save(depData, dep, "EDIT");

                    return CustomMessageHandler.RecordUpdated();
                }
            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
