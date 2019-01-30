using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.GradeDto;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class GradeService : IGradeService
    {
        private const string TABLE_NAME = "tbl_fsGrade";
        private readonly IGradeRepository repo;
        private readonly IAuditTrailService<Grade> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public GradeService(IGradeRepository repo,
             IAuditTrailService<Grade> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var Grade = await repo.GetById(id);
            if (Grade != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Grade(), Grade, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> DeleteByCode(string code)
        {
            var grade = await repo.GetByCode(code);
            if (grade != null)
            {
                await repo.DeleteByCode(code);

                await auditTrailService.Save(new Grade(), grade, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Grade grade)
        {
            // Get Company Code
            grade.CompanyCode = compInfoRepo.GetCompanyCode();
            if (String.IsNullOrEmpty(grade.Code) || String.IsNullOrWhiteSpace(grade.Code))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            var gradeData = await repo.GetByCode(grade.Code);

            if (gradeData == null)
            {
                var companyInfo = await compInfoRepo.GetByCompanyCode(compInfoRepo.GetCompanyCode());
                if (companyInfo == null)
                {
                    return CustomMessageHandler.Error("Company Information doesn't exist");
                }

                if (!(await compInfoRepo.CheckDBIfExists(companyInfo.PayrollDB)))
                {
                    return CustomMessageHandler.Error("Payroll Database doesn't exist!");
                }
                else if (!(await compInfoRepo.CheckDBIfExists(companyInfo.TKSDB)))
                {
                    return CustomMessageHandler.Error("Timekeeping Database doesn't exist!");
                }
                else if (!(await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB)))
                {
                    return CustomMessageHandler.Error("HRIS Database doesn't exist!");
                }
                else
                {
                    var gradeInsertDto = new GradeInsertToFileSetupDto
                    {
                        GrdCode = grade.Code,
                        GrdDesc = grade.Description,
                        CreatedBy = "" // Current User
                    };
                    // PAYROLL 
                    var result = await compInfoRepo.CheckTableIfExists(companyInfo.PayrollDB, TABLE_NAME);
                    if (result && companyInfo.PayrollFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromPayroll(grade.Code, companyInfo.PayrollDB);
                        if (results == null)
                        {
                            gradeInsertDto.DBName = companyInfo.PayrollDB;
                            // SAVE TO PAYROLL DB
                            await repo.InsertToPayrollFileSetUp(gradeInsertDto);
                        }

                    }

                    // TIME KEEPING
                    var tksResult = await compInfoRepo.CheckTableIfExists(companyInfo.TKSDB, TABLE_NAME);
                    if (tksResult && companyInfo.TKSFlag)
                    {
                        var results = await repo.GetByCodeFromTKS(grade.Code, companyInfo.TKSDB);
                        if (results == null)
                        {
                            gradeInsertDto.DBName = companyInfo.TKSDB;
                            // SAVE TO TKS DB
                            await repo.InsertToTSKFileSetUp(gradeInsertDto);
                        }

                    }
                    // HRIS
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        var results = await repo.GetByCodeFromHRIS(grade.Code, companyInfo.HRISDB);
                        if (results == null)
                        {
                            gradeInsertDto.DBName = companyInfo.HRISDB;
                            // SAVE TO HRIS DB
                            await repo.InsertToHRISFileSetUp(gradeInsertDto);
                        }

                    }
                }


                await repo.InsertFileSetup(grade);
                await repo.Insert(grade);
                await auditTrailService.Save(new Grade(), grade, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Grade Grade)
        {
            var GradeData = await repo.GetByCode(Grade.Code);
            if (GradeData != null)
            {
                await repo.Update(Grade);

                //Audit Trail
                await auditTrailService.Save(GradeData, Grade, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
