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
    public class DivisionService : IDivisionService
    {
        private readonly IDivisionRepository repo;
        private readonly IAuditTrailService<Division> auditTrailService;
        public DivisionService(IDivisionRepository repo,
             IAuditTrailService<Division> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var division = await repo.GetById(id);
            if (division != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Division(), division, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Division div)
        {
            if (String.IsNullOrEmpty(div.DivisionCode) || String.IsNullOrWhiteSpace(div.DivisionDesc))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(div.DivisionCode)) == null)
            {
                await repo.Insert(div);

                await auditTrailService.Save(new Division(), div, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Division div)
        {
            var divisionData = await repo.GetByCode(div.DivisionCode);
            if (divisionData != null)
            {
                await repo.Update(div);

                //Audit Trail
                await auditTrailService.Save(divisionData, div, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
