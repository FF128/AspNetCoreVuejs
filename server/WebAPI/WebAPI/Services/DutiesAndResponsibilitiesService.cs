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
    public class DutiesAndResponsibilitiesService : IDutiesAndResponsibilitiesService
    {
        private readonly IDutiesAndResponsibilitiesRepository repo;
        private readonly IAuditTrailService<DutiesAndResponsibilities> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public DutiesAndResponsibilitiesService(IDutiesAndResponsibilitiesRepository repo,
             IAuditTrailService<DutiesAndResponsibilities> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var payHouse = await repo.GetById(id);
            if (payHouse != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new DutiesAndResponsibilities(), payHouse, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(DutiesAndResponsibilities dar)
        {
            if (String.IsNullOrEmpty(dar.DutiesResponsibilitiesCode) || String.IsNullOrWhiteSpace(dar.DutiesResponsibilitiesCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(dar.DutiesResponsibilitiesCode)) == null)
            {
                dar.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(dar);

                await auditTrailService.Save(new DutiesAndResponsibilities(), dar, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(DutiesAndResponsibilities dar)
        {
            var payHouse = await repo.GetByCode(dar.DutiesResponsibilitiesCode);
            if (payHouse != null)
            {
                await repo.Update(dar);

                //Audit Trail
                await auditTrailService.Save(payHouse, dar, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
