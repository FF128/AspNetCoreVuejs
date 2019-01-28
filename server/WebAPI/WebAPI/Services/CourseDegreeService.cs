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
    public class CourseDegreeService : ICourseDegreeService
    {
        private readonly ICourseDegreeRepository repo;
        private readonly IAuditTrailService<CourseDegree> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public CourseDegreeService(ICourseDegreeRepository repo,
             IAuditTrailService<CourseDegree> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
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

        public async Task<CustomMessage> Insert(CourseDegree cd)
        {
            if (String.IsNullOrEmpty(cd.CourseDegreeCode) || String.IsNullOrWhiteSpace(cd.CourseDegreeCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(cd.CourseDegreeCode)) == null)
            {
                cd.CompanyCode = compInfoRepo.GetCompanyCode();
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
