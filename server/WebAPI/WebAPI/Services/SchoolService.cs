using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.School;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class SchoolService : ISchoolService
    {
        private const string TABLE_NAME = "tbl_fsSchool";
        private readonly ISchoolRepository repo;
        private readonly IAuditTrailService<School> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IFileSetupService fileSetupService;
        private readonly IMapper mapper;
        public SchoolService(ISchoolRepository repo,
             IAuditTrailService<School> auditTrailService,
             ICompanyInformationRepository compInfoRepo,
             IMapper mapper,
             IFileSetupService fileSetupService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
            this.mapper = mapper;
            this.fileSetupService = fileSetupService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var schoolData = await repo.GetById(id);
            if (schoolData != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new School(), schoolData, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> DeleteByCode(string code)
        {
            var school = await repo.GetByCode(code);
            if (school != null)
            {
                await repo.DeleteByCode(code);

                await auditTrailService.Save(new School(), school, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(School school)
        {
            // Get Company Code
            school.CompanyCode = compInfoRepo.GetCompanyCode();
            if (String.IsNullOrEmpty(school.SchoolCode) || String.IsNullOrWhiteSpace(school.SchoolCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            var schoolData = await repo.GetByCode(school.SchoolCode);

            if (schoolData == null)
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
                    // HRIS
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        var results = await repo.GetByCodeFromHRIS(school.SchoolCode, companyInfo.HRISDB);
                        if (results == null)
                        {
                            var schoolDto = mapper.Map<SchoolInsertToHRISFSDto>(school);
                            schoolDto.DBName = companyInfo.HRISDB;
                            // SAVE TO HRIS DB
                            await repo.InsertToHRISFileSetUp(schoolDto);
                        }

                    }
                }


                await repo.InsertFileSetup(school);
                await repo.Insert(school);
                await auditTrailService.Save(new School(), school, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(School school)
        {
            var schoolData = await repo.GetByCode(school.SchoolCode);
            if (schoolData != null)
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
                    // HRIS 
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromHRIS(school.SchoolCode, companyInfo.HRISDB);
                        if (results != null)
                        {
                            var schoolDto = mapper.Map<SchoolUpdateToHRISFSDto>(school);
                            schoolDto.DBName = companyInfo.HRISDB;
                            // SAVE TO HRIS DB
                            await repo.UpdateToHRISFileSetUp(schoolDto);
                        }
                    }

                    await repo.UpdateFileSetup(school);
                    await repo.Update(school);

                    //Audit Trail
                    await auditTrailService.Save(schoolData, school, "EDIT");

                    return CustomMessageHandler.RecordUpdated();
                }
            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
