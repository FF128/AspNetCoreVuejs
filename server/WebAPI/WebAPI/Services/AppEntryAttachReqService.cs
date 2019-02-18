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
    public class AppEntryAttachReqService : IAppEntryAttachReqService
    {
        private readonly IAppEntryAttachReqRepository repo;
        private readonly IAuditTrailService<AppEntryAttachReq> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IUserRepository userRepo;
        public AppEntryAttachReqService(IAppEntryAttachReqRepository repo,
             IAuditTrailService<AppEntryAttachReq> auditTrailService,
             ICompanyInformationRepository compInfoRepo,
             IUserRepository userRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
            this.userRepo = userRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var attachReq = await repo.GetById(id);
            if (attachReq != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new AppEntryAttachReq(), attachReq, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(AppEntryAttachReq attachReq)
        {
            if ((await repo.GetById(attachReq.Id)) == null)
            {
                attachReq.CreatedBy = userRepo.GetEmpCode();
                attachReq.CompanyCode = compInfoRepo.GetCompanyCode(); // Get Company Code of Current User
                await repo.Insert(attachReq);

                await auditTrailService.Save(new AppEntryAttachReq(), attachReq, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(AppEntryAttachReq attachReq)
        {
            var attachReqData = await repo.GetById(attachReq.Id);
            if (attachReqData != null)
            {
                attachReq.EditedBy = userRepo.GetEmpCode();
                await repo.Update(attachReq);

                //Audit Trail
                await auditTrailService.Save(attachReqData, attachReq, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("Data not found");
        }
    }
}
