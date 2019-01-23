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
    public class JobGroupService : IJobGroupService
    {
        private readonly IJobGroupRepository repo;
        private readonly IAuditTrailService<JobGroup> auditTrailService;
        public JobGroupService(IJobGroupRepository repo,
             IAuditTrailService<JobGroup> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var jobGroup = await repo.GetById(id);
            if (jobGroup != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new JobGroup(), jobGroup, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(JobGroup jg)
        {
            if (String.IsNullOrEmpty(jg.JobGroupCode) || String.IsNullOrWhiteSpace(jg.JobGroupCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(jg.JobGroupCode)) == null)
            {
                await repo.Insert(jg);

                await auditTrailService.Save(new JobGroup(), jg, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(JobGroup jg)
        {
            var jobGroup = await repo.GetByCode(jg.JobGroupCode);
            if (jobGroup != null)
            {
                await repo.Update(jg);

                //Audit Trail
                await auditTrailService.Save(jobGroup, jg, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
