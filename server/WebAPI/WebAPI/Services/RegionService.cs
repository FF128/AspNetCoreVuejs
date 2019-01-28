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
        private readonly ICompanyInformationRepository compInfoRepo;
        public RegionService(IRegionRepository repo,
             IAuditTrailService<Region> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
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

        public async Task<CustomMessage> Insert(Region reg)
        {
            if (String.IsNullOrEmpty(reg.RegionCode) || String.IsNullOrWhiteSpace(reg.RegionCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(reg.RegionCode)) == null)
            {
                reg.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(reg);

                await auditTrailService.Save(new Region(), reg, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Region reg)
        {
            var region = await repo.GetByCode(reg.RegionCode);
            if (region != null)
            {
                await repo.Update(reg);

                //Audit Trail
                await auditTrailService.Save(region, reg, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
