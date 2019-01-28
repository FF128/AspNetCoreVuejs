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
    public class LevelsOfEmployeeService : ILevelsOfEmployeeService
    {
        private readonly ILevelsOfEmployeeRepository repo;
        private readonly IAuditTrailService<LevelsOfEmployee> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public LevelsOfEmployeeService(ILevelsOfEmployeeRepository repo,
             IAuditTrailService<LevelsOfEmployee> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var loeData = await repo.GetById(id);
            if (loeData != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new LevelsOfEmployee(), loeData, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(LevelsOfEmployee loe)
        {
            if (String.IsNullOrEmpty(loe.LOECode) || String.IsNullOrWhiteSpace(loe.LOECode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(loe.LOECode)) == null)
            {
                loe.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(loe);

                await auditTrailService.Save(new LevelsOfEmployee(), loe, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(LevelsOfEmployee loe)
        {
            var loeData = await repo.GetByCode(loe.LOECode);
            if (loeData != null)
            {
                await repo.Update(loe);

                //Audit Trail
                await auditTrailService.Save(loeData, loe, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
