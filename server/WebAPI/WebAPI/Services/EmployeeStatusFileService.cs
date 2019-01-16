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
    public class EmployeeStatusFileService : IEmployeeStatusFileService
    {
        private readonly IEmployeeStatusFileRepository repo;
        private readonly IAuditTrailRepository auditTrailRepo;
        private readonly IAuditTrailService<EmployeeStatusFile> auditTrailService;
        public EmployeeStatusFileService(IEmployeeStatusFileRepository repo,
            IAuditTrailRepository auditTrailRepo,
             IAuditTrailService<EmployeeStatusFile> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailRepo = auditTrailRepo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var esf = await repo.GetById(id);
            if (esf != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new EmployeeStatusFile(), esf, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(EmployeeStatusFile esf)
        {
            var esfData = await repo.GetByCode(esf.Code);
            if (esfData == null)
            {
                await repo.Insert(esf);

                await auditTrailService.Save(new EmployeeStatusFile(), esf, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(EmployeeStatusFile esf)
        {
            var esfData = await repo.GetByCode(esf.Code);
            if (esfData != null)
            {
                await repo.Update(esf);

                //Audit Trail
                await auditTrailService.Save(esfData, esf, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }

    }
}
