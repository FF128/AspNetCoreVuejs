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
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository repo;
        private readonly IAuditTrailService<School> auditTrailService;
        public SchoolService(ISchoolRepository repo,
             IAuditTrailService<School> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var payHouse = await repo.GetById(id);
            if (payHouse != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new School(), payHouse, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(School ph)
        {
            if (String.IsNullOrEmpty(ph.SchoolCode) || String.IsNullOrWhiteSpace(ph.SchoolCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(ph.SchoolCode)) == null)
            {
                await repo.Insert(ph);

                await auditTrailService.Save(new School(), ph, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(School ph)
        {
            var payHouse = await repo.GetByCode(ph.SchoolCode);
            if (payHouse != null)
            {
                await repo.Update(ph);

                //Audit Trail
                await auditTrailService.Save(payHouse, ph, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
