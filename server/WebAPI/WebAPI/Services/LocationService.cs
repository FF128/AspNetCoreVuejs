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
        private readonly ICompanyInformationRepository compInfoRepo;
        public LocationService(ILocationRepository repo,
             IAuditTrailService<Location> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
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

        public async Task<CustomMessage> Insert(Location loc)
        {
            if (String.IsNullOrEmpty(loc.LocationCode) || String.IsNullOrWhiteSpace(loc.LocationDesc))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(loc.LocationCode)) == null)
            {
                loc.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(loc);

                await auditTrailService.Save(new Location(), loc, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Location loc)
        {
            var locData = await repo.GetByCode(loc.LocationCode);
            if (locData != null)
            {
                await repo.Update(loc);

                //Audit Trail
                await auditTrailService.Save(locData, loc, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
