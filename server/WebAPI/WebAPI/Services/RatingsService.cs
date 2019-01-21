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
    public class RatingsService : IRatingsService
    {
        private readonly IRatingsRepository repo;
        private readonly IAuditTrailService<Ratings> auditTrailService;
        private readonly ICompanyInfoService comInfoService;
        public RatingsService(IRatingsRepository repo,
             IAuditTrailService<Ratings> auditTrailService,
             ICompanyInfoService comInfoService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.comInfoService = comInfoService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var stData = await repo.GetById(id);
            if (stData != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Ratings(), stData, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Ratings st)
        {
            if (String.IsNullOrEmpty(st.RatingsCode) || String.IsNullOrWhiteSpace(st.RatingsCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(st.RatingsCode)) == null)
            {
                st.CompanyCode = comInfoService.CompanyCode;
                await repo.Insert(st);

                await auditTrailService.Save(new Ratings(), st, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Ratings st)
        {
            var stData = await repo.GetByCode(st.RatingsCode);
            if (stData != null)
            {
                await repo.Update(st);

                //Audit Trail
                await auditTrailService.Save(stData, st, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
