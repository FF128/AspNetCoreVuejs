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
    public class PayHouseService : IPayHouseService
    {
        private readonly IPayHouseRepository repo;
        private readonly IAuditTrailService<PayHouse> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public PayHouseService(IPayHouseRepository repo,
             IAuditTrailService<PayHouse> auditTrailService,
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

                await auditTrailService.Save(new PayHouse(), payHouse, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(PayHouse ph)
        {
            if (String.IsNullOrEmpty(ph.PayHouseCode) || String.IsNullOrWhiteSpace(ph.PayHouseCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(ph.PayHouseCode)) == null)
            {
                ph.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(ph);

                await auditTrailService.Save(new PayHouse(), ph, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(PayHouse ph)
        {
            var payHouse = await repo.GetByCode(ph.PayHouseCode);
            if (payHouse != null)
            {
                await repo.Update(ph);

                //Audit Trail
                await auditTrailService.Save(payHouse, ph, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
