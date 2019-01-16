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
    public class StepService : IStepService
    {
        private readonly IStepRepository repo;
        private readonly IAuditTrailService<Step> auditTrailService;
        public StepService(IStepRepository repo,
             IAuditTrailService<Step> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var Step = await repo.GetById(id);
            if (Step != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Step(), Step, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Step step)
        {
            if (String.IsNullOrEmpty(step.Code) || String.IsNullOrWhiteSpace(step.Code))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(step.Code)) == null)
            {
                await repo.Insert(step);

                await auditTrailService.Save(new Step(), step, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Step step)
        {
            var StepData = await repo.GetByCode(step.Code);
            if (StepData != null)
            {
                await repo.Update(step);

                //Audit Trail
                await auditTrailService.Save(StepData, step, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
