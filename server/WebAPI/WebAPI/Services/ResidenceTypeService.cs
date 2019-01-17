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
    public class ResidenceTypeService : IResidenceTypeService
    {
        private readonly IResidenceTypeRepository repo;
        private readonly IAuditTrailService<ResidenceType> auditTrailService;
        public ResidenceTypeService(IResidenceTypeRepository repo,
             IAuditTrailService<ResidenceType> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var resType = await repo.GetById(id);
            if (resType != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new ResidenceType(), resType, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(ResidenceType rt)
        {
            if (String.IsNullOrEmpty(rt.ResidenceTypeCode) || String.IsNullOrWhiteSpace(rt.ResidenceTypeCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(rt.ResidenceTypeCode)) == null)
            {
                await repo.Insert(rt);

                await auditTrailService.Save(new ResidenceType(), rt, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(ResidenceType rt)
        {
            var resType = await repo.GetByCode(rt.ResidenceTypeCode);
            if (resType != null)
            {
                await repo.Update(rt);

                //Audit Trail
                await auditTrailService.Save(resType, rt, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
