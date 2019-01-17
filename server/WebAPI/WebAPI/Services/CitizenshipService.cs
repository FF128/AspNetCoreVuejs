using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;
using WebAPI.Utilities;
using _128Utility.Network.Machine;

namespace WebAPI.Services
{
    public class CitizenshipService : ICitizenshipService
    {
        private readonly ICitizenshipRepository repo;
        private readonly IAuditTrailRepository auditTrailRepo;
        private readonly IAuditTrailService<Citizenship> auditTrailService;
        public CitizenshipService(ICitizenshipRepository repo,
            IAuditTrailRepository auditTrailRepo,
             IAuditTrailService<Citizenship> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailRepo = auditTrailRepo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var cit = await repo.GetById(id);
            if (cit != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Citizenship(), cit, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> DeleteByCode(string code)
        {
            var cit = await repo.GetByCode(code);
            if (cit != null)
            {
                await repo.DeleteByCode(code);

                await auditTrailService.Save(new Citizenship(), cit, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Citizenship cit)
        {
            if (String.IsNullOrEmpty(cit.CitiCode) || String.IsNullOrWhiteSpace(cit.CitiCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }
            if ((await repo.GetByCode(cit.CitiCode)) == null)
            {
                await repo.Insert(cit);

                await auditTrailService.Save(new Citizenship(), cit, "ADD");

                return CustomMessageHandler.RecordAdded();
               
            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Citizenship cit)
        {
            var citData = await repo.GetByCode(cit.CitiCode);
            if (citData != null)
            {
                await repo.Update(cit);

                //Audit Trail
                await auditTrailService.Save(citData, cit, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }

    }
}
