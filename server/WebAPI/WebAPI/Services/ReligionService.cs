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
    public class ReligionService : IReligionService
    {
        private readonly IReligionRepository repo;
        private readonly IAuditTrailService<Religion> auditTrailService;
        public ReligionService(IReligionRepository repo,
            IAuditTrailService<Religion> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var rel = await repo.GetById(id);
            if (rel != null)
            {
                await repo.Delete(id);

                //Audit Trail
                await auditTrailService.Save(new Religion(), rel, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Religion rel)
        {
            if ((await repo.GetByCode(rel.Code)) == null)
            {
                await repo.Insert(rel);

                //Audit Trail
                await auditTrailService.Save(new Religion(), rel, "ADD");

                return CustomMessageHandler.RecordAdded();
            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Religion rel)
        {
            var oldVal = await repo.GetById(rel.Id);
            if ((await repo.GetByCode(rel.Code)) != null)
            {
                await repo.Update(rel);

                //Audit Trail
                await auditTrailService.Save(oldVal, rel, "EDIT");

                return CustomMessageHandler.RecordUpdated();
            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
