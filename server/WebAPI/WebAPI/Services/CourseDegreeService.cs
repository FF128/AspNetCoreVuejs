using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Dtos.CourseDto;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class CourseDegreeService : ICourseDegreeService
    {
        private const string TABLE_NAME = "tbl_fsCourseDegree";
        private readonly ICourseDegreeRepository repo;
        private readonly IAuditTrailService<CourseDegree> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IFileSetupService fileSetupService;
        private readonly IMapper mapper;
        public CourseDegreeService(ICourseDegreeRepository repo,
             IAuditTrailService<CourseDegree> auditTrailService,
             ICompanyInformationRepository compInfoRepo,
             IFileSetupService fileSetupService,
             IMapper mapper)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
            this.mapper = mapper;
            this.fileSetupService = fileSetupService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var courseDegree = await repo.GetById(id);
            if (courseDegree != null)
            {
                //await repo.Delete(id);

                await auditTrailService.Save(new CourseDegree(), courseDegree, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> DeleteByCode(string code)
        {
            var course = await repo.GetByCode(code);
            if (course != null)
            {
                var companyInfo = await compInfoRepo.GetByCompanyCode(compInfoRepo.GetCompanyCode());
                var validationResult =
                    fileSetupService.Validate(companyInfo, await compInfoRepo.CheckDBIfExists(companyInfo.PayrollDB),
                        await compInfoRepo.CheckDBIfExists(companyInfo.TKSDB), await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB));

                if (!validationResult.hasError)
                {
                    return CustomMessageHandler.Error(validationResult.message);
                }

                // HRIS
                var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                if (hrisResult && companyInfo.HRISFlag)
                {
                    var results = await repo.GetByCodeFromHRIS(course.CourseDegreeCode, companyInfo.HRISDB);
                    if (results != null)
                    {
                        // SAVE TO HRIS DB
                        await repo.DeleteFromHRISFileSetUp(new DeleteSetUpDto
                        {
                            Code = course.CourseDegreeCode,
                            DBName = companyInfo.HRISDB
                        });
                    }
                }

                await repo.DeleteFileSetUp(course.CourseDegreeCode);
                await repo.Delete(course.CourseDegreeCode);


                await auditTrailService.Save(new CourseDegree(), course, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(CourseDegree cd)
        {
            // Get Company Code
            cd.CompanyCode = compInfoRepo.GetCompanyCode();
            if (String.IsNullOrEmpty(cd.CourseDegreeCode) || String.IsNullOrWhiteSpace(cd.CourseDegreeCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            var cdData = await repo.GetByCode(cd.CourseDegreeCode);

            if (cdData == null)
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
                        var results = await repo.GetByCodeFromHRIS(cd.CourseDegreeCode, companyInfo.HRISDB);
                        if (results == null)
                        {
                            var courseDto = mapper.Map<CourseInsertToHRISFSDto>(cd);
                            courseDto.DBName = companyInfo.HRISDB;
                            // SAVE TO HRIS DB
                            await repo.InsertToHRISFileSetUp(courseDto);
                        }

                    }
                }


                await repo.InsertFileSetup(cd);
                await repo.Insert(cd);
                await auditTrailService.Save(new CourseDegree(), cd, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(CourseDegree cd)
        {
            var cdData = await repo.GetByCode(cd.CourseDegreeCode);
            if (cdData != null)
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
                        var results = await repo.GetByCodeFromHRIS(cd.CourseDegreeCode, companyInfo.HRISDB);
                        if (results != null)
                        {
                            var courseDto = mapper.Map<CourseUpdateToHRISFSDto>(cd);
                            courseDto.DBName = companyInfo.HRISDB;
                            // SAVE TO HRIS DB
                            await repo.UpdateToHRISFileSetUp(courseDto);
                        }
                    }

                    await repo.UpdateFileSetup(cd);
                    await repo.Update(cd);

                    //Audit Trail
                    await auditTrailService.Save(cdData, cd, "EDIT");

                    return CustomMessageHandler.RecordUpdated();
                }
            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
