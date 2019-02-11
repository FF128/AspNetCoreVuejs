using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models.ApplicantsEntrySetUp;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class AppEntrySourceService : IAppEntrySourceService
    {
        private readonly IAppEntrySourceRepository repo;
        private readonly IAuditTrailService<AppEntrySource> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public AppEntrySourceService(IAppEntrySourceRepository repo,
             IAuditTrailService<AppEntrySource> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var attachReq = await repo.GetById(id);
            if (attachReq != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new AppEntrySource(), attachReq, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(AppEntrySource attachReq)
        {
            if ((await repo.GetById(attachReq.Id)) == null)
            {
                attachReq.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(attachReq);

                await auditTrailService.Save(new AppEntrySource(), attachReq, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(AppEntrySource attachReq)
        {
            var attachReqData = await repo.GetById(attachReq.Id);
            if (attachReqData != null)
            {
                await repo.Update(attachReq);

                //Audit Trail
                await auditTrailService.Save(attachReqData, attachReq, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("Data not found");
        }
    }
}
