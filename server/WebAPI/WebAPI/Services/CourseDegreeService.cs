using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IMapper mapper;
        public CourseDegreeService(ICourseDegreeRepository repo,
             IAuditTrailService<CourseDegree> auditTrailService,
             ICompanyInformationRepository compInfoRepo,
             IMapper mapper)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
            this.mapper = mapper;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var courseDegree = await repo.GetById(id);
            if (courseDegree != null)
            {
                await repo.Delete(id);

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
                await repo.DeleteByCode(code);

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
            var courseDegree = await repo.GetByCode(cd.CourseDegreeCode);
            if (courseDegree != null)
            {
                await repo.Update(cd);

                //Audit Trail
                await auditTrailService.Save(courseDegree, cd, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
