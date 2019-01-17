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
    public class PersonnelRequestTypeService : IPersonnelRequestTypeService
    {
        private readonly IPersonnelRequestTypeRepository repo;
        private readonly IAuditTrailService<PersonnelRequestType> auditTrailService;
        public PersonnelRequestTypeService(IPersonnelRequestTypeRepository repo,
             IAuditTrailService<PersonnelRequestType> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var prtData = await repo.GetById(id);
            if (prtData != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new PersonnelRequestType(), prtData, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(PersonnelRequestType prt)
        {
            if (String.IsNullOrEmpty(prt.PersonnelReqTypeCode) || String.IsNullOrWhiteSpace(prt.PersonnelReqTypeCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(prt.PersonnelReqTypeCode)) == null)
            {
                await repo.Insert(prt);

                await auditTrailService.Save(new PersonnelRequestType(), prt, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(PersonnelRequestType prt)
        {
            var prtData = await repo.GetByCode(prt.PersonnelReqTypeCode);
            if (prtData != null)
            {
                await repo.Update(prt);

                //Audit Trail
                await auditTrailService.Save(prtData, prt, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
