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
    public class ProjectCodeService : IProjectCodeService
    {
        private readonly IProjectCodeRepository repo;
        private readonly IAuditTrailService<ProjectCodeModel> auditTrailService;
        public ProjectCodeService(IProjectCodeRepository repo,
             IAuditTrailService<ProjectCodeModel> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var projectCode = await repo.GetById(id);
            if (projectCode != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new ProjectCodeModel(), projectCode, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(ProjectCodeModel proj)
        {
            if (String.IsNullOrEmpty(proj.ProjectCode) || String.IsNullOrWhiteSpace(proj.ProjectDesc))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(proj.ProjectCode)) == null)
            {
                await repo.Insert(proj);

                await auditTrailService.Save(new ProjectCodeModel(), proj, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(ProjectCodeModel proj)
        {
            var projectCode = await repo.GetByCode(proj.ProjectCode);
            if (projectCode != null)
            {
                await repo.Update(proj);

                //Audit Trail
                await auditTrailService.Save(projectCode, proj, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
