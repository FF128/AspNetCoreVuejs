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
    public class DocSubmittedService : IDocSubmittedService
    {
        private readonly IDocSubmittedRepository repo;
        private readonly IAuditTrailService<DocSubmitted> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public DocSubmittedService(IDocSubmittedRepository repo,
             IAuditTrailService<DocSubmitted> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var docSubmitted = await repo.GetById(id);
            if (docSubmitted != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new DocSubmitted(), docSubmitted, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(DocSubmitted dc)
        {
            if (String.IsNullOrEmpty(dc.DocCode) || String.IsNullOrWhiteSpace(dc.DocCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(dc.DocCode)) == null)
            {
                dc.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(dc);

                await auditTrailService.Save(new DocSubmitted(), dc, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(DocSubmitted dc)
        {
            var docSubmitted = await repo.GetByCode(dc.DocCode);
            if (docSubmitted != null)
            {
                await repo.Update(dc);

                //Audit Trail
                await auditTrailService.Save(docSubmitted, dc, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
