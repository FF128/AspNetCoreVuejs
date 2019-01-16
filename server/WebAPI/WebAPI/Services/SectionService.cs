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
        public SectionService(ISectionRepository repo,
             IAuditTrailService<Section> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
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

        public async Task<CustomMessage> Insert(Section div)
        {
            if (String.IsNullOrEmpty(div.SectionCode) || String.IsNullOrWhiteSpace(div.SectionDesc))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(div.SectionCode)) == null)
            {
                await repo.Insert(div);

                await auditTrailService.Save(new Section(), div, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Section div)
        {
            var sectionData = await repo.GetByCode(div.SectionCode);
            if (sectionData != null)
            {
                await repo.Update(div);

                //Audit Trail
                await auditTrailService.Save(sectionData, div, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
