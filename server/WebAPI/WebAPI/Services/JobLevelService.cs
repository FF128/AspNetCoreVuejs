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
    public class JobLevelService : IJobLevelService
    {
        private readonly IJobLevelRepository repo;
        private readonly IAuditTrailService<JobLevel> auditTrailService;
        public JobLevelService(IJobLevelRepository repo,
             IAuditTrailService<JobLevel> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var jobLevel = await repo.GetById(id);
            if (jobLevel != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new JobLevel(), jobLevel, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(JobLevel jobLevel)
        {
            if (String.IsNullOrEmpty(jobLevel.Code) || String.IsNullOrWhiteSpace(jobLevel.Code))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(jobLevel.Code)) == null)
            {
                await repo.Insert(jobLevel);

                await auditTrailService.Save(new JobLevel(), jobLevel, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(JobLevel jobLevel)
        {
            var jobLevelData = await repo.GetByCode(jobLevel.Code);
            if (jobLevelData != null)
            {
                await repo.Update(jobLevel);

                //Audit Trail
                await auditTrailService.Save(jobLevelData, jobLevel, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
