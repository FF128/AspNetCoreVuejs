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
    public class GovExamsService : IGovExamsService
    {
        private readonly IGovExamsRepository repo;
        private readonly IAuditTrailService<GovExams> auditTrailService;
        public GovExamsService(IGovExamsRepository repo,
             IAuditTrailService<GovExams> auditTrailService)
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

                await auditTrailService.Save(new GovExams(), payHouse, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(GovExams ge)
        {
            if (String.IsNullOrEmpty(ge.ExamCode) || String.IsNullOrWhiteSpace(ge.ExamCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(ge.ExamCode)) == null)
            {
                await repo.Insert(ge);

                await auditTrailService.Save(new GovExams(), ge, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(GovExams ge)
        {
            var payHouse = await repo.GetByCode(ge.ExamCode);
            if (payHouse != null)
            {
                await repo.Update(ge);

                //Audit Trail
                await auditTrailService.Save(payHouse, ge, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
