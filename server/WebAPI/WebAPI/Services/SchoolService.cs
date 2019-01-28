using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository repo;
        private readonly IAuditTrailService<School> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public SchoolService(ISchoolRepository repo,
             IAuditTrailService<School> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
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

        public async Task<CustomMessage> Insert(School school)
        {
            if (String.IsNullOrEmpty(school.SchoolCode) || String.IsNullOrWhiteSpace(school.SchoolCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(school.SchoolCode)) == null)
            {
                school.CompanyCode = compInfoRepo.GetCompanyCode();
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
                await repo.Update(school);

                //Audit Trail
                await auditTrailService.Save(schoolData, school, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
