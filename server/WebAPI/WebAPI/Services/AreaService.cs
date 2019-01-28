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
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository repo;
        private readonly IAuditTrailService<Area> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public AreaService(IAreaRepository repo,
             IAuditTrailService<Area> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var Area = await repo.GetById(id);
            if (Area != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Area(), Area, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Area area)
        {
            if (String.IsNullOrEmpty(area.AreaCode) || String.IsNullOrWhiteSpace(area.AreaCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(area.AreaCode)) == null)
            {
                area.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(area);

                await auditTrailService.Save(new Area(), area, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Area area)
        {
            var AreaData = await repo.GetByCode(area.AreaCode);
            if (AreaData != null)
            {
                await repo.Update(area);

                //Audit Trail
                await auditTrailService.Save(AreaData, area, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
