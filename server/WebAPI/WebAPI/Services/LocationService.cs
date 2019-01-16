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
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository repo;
        private readonly IAuditTrailService<Location> auditTrailService;
        public LocationService(ILocationRepository repo,
             IAuditTrailService<Location> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var location = await repo.GetById(id);
            if (location != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Location(), location, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Location dep)
        {
            if (String.IsNullOrEmpty(dep.LocationCode) || String.IsNullOrWhiteSpace(dep.LocationDesc))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(dep.LocationCode)) == null)
            {
                await repo.Insert(dep);

                await auditTrailService.Save(new Location(), dep, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Location dep)
        {
            var locData = await repo.GetByCode(dep.LocationCode);
            if (locData != null)
            {
                await repo.Update(dep);

                //Audit Trail
                await auditTrailService.Save(locData, dep, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
