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
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository repo;
        private readonly IAuditTrailService<Section> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public SectionService(ISectionRepository repo,
             IAuditTrailService<Section> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var section = await repo.GetById(id);
            if (section != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Section(), section, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Section sec)
        {
            if (String.IsNullOrEmpty(sec.SectionCode) || String.IsNullOrWhiteSpace(sec.SectionDesc))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(sec.SectionCode)) == null)
            {
                sec.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(sec);

                await auditTrailService.Save(new Section(), sec, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Section sec)
        {
            var sectionData = await repo.GetByCode(sec.SectionCode);
            if (sectionData != null)
            {
                await repo.Update(sec);

                //Audit Trail
                await auditTrailService.Save(sectionData, sec, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
