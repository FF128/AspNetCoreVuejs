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
    public class MajorService : IMajorService
    {
        private readonly IMajorRepository repo;
        private readonly IAuditTrailService<Major> auditTrailService;
        public MajorService(IMajorRepository repo,
             IAuditTrailService<Major> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var major = await repo.GetById(id);
            if (major != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Major(), major, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Major maj)
        {
            if (String.IsNullOrEmpty(maj.MajorCode) || String.IsNullOrWhiteSpace(maj.MajorCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(maj.MajorCode)) == null)
            {
                await repo.Insert(maj);

                await auditTrailService.Save(new Major(), maj, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Major maj)
        {
            var major = await repo.GetByCode(maj.MajorCode);
            if (major != null)
            {
                await repo.Update(maj);

                //Audit Trail
                await auditTrailService.Save(major, maj, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
