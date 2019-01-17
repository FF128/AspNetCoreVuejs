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
    public class SkillsService : ISkillsService
    {
        private readonly ISkillsRepository repo;
        private readonly IAuditTrailService<Skills> auditTrailService;
        public SkillsService(ISkillsRepository repo,
             IAuditTrailService<Skills> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var skillData = await repo.GetById(id);
            if (skillData != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Skills(), skillData, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Skills skill)
        {
            if (String.IsNullOrEmpty(skill.SkillsCode) || String.IsNullOrWhiteSpace(skill.SkillsCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(skill.SkillsCode)) == null)
            {
                await repo.Insert(skill);

                await auditTrailService.Save(new Skills(), skill, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Skills skill)
        {
            var skillData = await repo.GetByCode(skill.SkillsCode);
            if (skillData != null)
            {
                await repo.Update(skill);

                //Audit Trail
                await auditTrailService.Save(skillData, skill, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
