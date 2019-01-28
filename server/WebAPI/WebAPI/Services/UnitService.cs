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
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository repo;
        private readonly IAuditTrailService<Unit> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public UnitService(IUnitRepository repo,
             IAuditTrailService<Unit> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var unit = await repo.GetById(id);
            if (unit != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Unit(), unit, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Unit unit)
        {
            if (String.IsNullOrEmpty(unit.UnitCode) || String.IsNullOrWhiteSpace(unit.UnitDesc))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(unit.UnitCode)) == null)
            {
                unit.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(unit);

                await auditTrailService.Save(new Unit(), unit, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Unit unit)
        {
            var unitData = await repo.GetByCode(unit.UnitCode);
            if (unitData != null)
            {
                await repo.Update(unit);

                //Audit Trail
                await auditTrailService.Save(unitData, unit, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
