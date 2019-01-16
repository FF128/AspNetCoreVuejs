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
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository repo;
        private readonly IAuditTrailService<Grade> auditTrailService;
        public GradeService(IGradeRepository repo,
             IAuditTrailService<Grade> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var Grade = await repo.GetById(id);
            if (Grade != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Grade(), Grade, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Grade Grade)
        {
            if (String.IsNullOrEmpty(Grade.Code) || String.IsNullOrWhiteSpace(Grade.Code))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(Grade.Code)) == null)
            {
                await repo.Insert(Grade);

                await auditTrailService.Save(new Grade(), Grade, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Grade Grade)
        {
            var GradeData = await repo.GetByCode(Grade.Code);
            if (GradeData != null)
            {
                await repo.Update(Grade);

                //Audit Trail
                await auditTrailService.Save(GradeData, Grade, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
