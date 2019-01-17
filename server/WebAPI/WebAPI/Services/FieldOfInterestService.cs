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
    public class FieldOfInterestService : IFieldOfInterestService
    {
        private readonly IFieldOfInterestRepository repo;
        private readonly IAuditTrailService<FieldOfInterest> auditTrailService;
        public FieldOfInterestService(IFieldOfInterestRepository repo,
             IAuditTrailService<FieldOfInterest> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var payHouse = await repo.GetById(id);
            if (payHouse != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new FieldOfInterest(), payHouse, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(FieldOfInterest foi)
        {
            if (String.IsNullOrEmpty(foi.InterestCode) || String.IsNullOrWhiteSpace(foi.InterestCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(foi.InterestCode)) == null)
            {
                await repo.Insert(foi);

                await auditTrailService.Save(new FieldOfInterest(), foi, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(FieldOfInterest foi)
        {
            var payHouse = await repo.GetByCode(foi.InterestCode);
            if (payHouse != null)
            {
                await repo.Update(foi);

                //Audit Trail
                await auditTrailService.Save(payHouse, foi, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
