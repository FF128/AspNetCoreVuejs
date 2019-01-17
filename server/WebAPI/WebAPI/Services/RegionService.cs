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
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository repo;
        private readonly IAuditTrailService<Region> auditTrailService;
        public RegionService(IRegionRepository repo,
             IAuditTrailService<Region> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var region = await repo.GetById(id);
            if (region != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Region(), region, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Region ph)
        {
            if (String.IsNullOrEmpty(ph.RegionCode) || String.IsNullOrWhiteSpace(ph.RegionCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(ph.RegionCode)) == null)
            {
                await repo.Insert(ph);

                await auditTrailService.Save(new Region(), ph, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Region ph)
        {
            var region = await repo.GetByCode(ph.RegionCode);
            if (region != null)
            {
                await repo.Update(ph);

                //Audit Trail
                await auditTrailService.Save(region, ph, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
