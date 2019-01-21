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
    public class OverallRatingsService : IOverallRatingsService
    {
        private readonly IOverallRatingsRepository repo;
        private readonly IAuditTrailService<OverallRatings> auditTrailService;
        private readonly ICompanyInfoService comInfoService;
        public OverallRatingsService(IOverallRatingsRepository repo,
             IAuditTrailService<OverallRatings> auditTrailService,
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

                await auditTrailService.Save(new OverallRatings(), stData, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(OverallRatings st)
        {
            if (String.IsNullOrEmpty(st.OverallRatingsCode) || String.IsNullOrWhiteSpace(st.OverallRatingsCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(st.OverallRatingsCode)) == null)
            {
                st.CompanyCode = comInfoService.CompanyCode;
                await repo.Insert(st);

                await auditTrailService.Save(new OverallRatings(), st, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(OverallRatings st)
        {
            var stData = await repo.GetByCode(st.OverallRatingsCode);
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
