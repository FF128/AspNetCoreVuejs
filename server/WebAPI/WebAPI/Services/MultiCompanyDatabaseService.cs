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
    public class MultiCompanyDatabaseService : IMultiCompanyDatabaseService
    {
        private readonly IMultiCompanyDatabaseRepository repo;
        private readonly IAuditTrailService<MultiCompanyDatabase> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public MultiCompanyDatabaseService(IMultiCompanyDatabaseRepository repo,
             IAuditTrailService<MultiCompanyDatabase> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
        }

        public async Task<CustomMessage> Delete(string code)
        {
            var multiCompDb = await repo.GetByCode(code);
            if (multiCompDb != null)
            {
                await repo.Delete(code);

                await auditTrailService.Save(new MultiCompanyDatabase(), multiCompDb, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(MultiCompanyDatabase multiComp)
        {
            if (String.IsNullOrEmpty(multiComp.DatabaseCode) || String.IsNullOrWhiteSpace(multiComp.DatabaseCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(multiComp.DatabaseCode)) == null)
            {
                multiComp.CompanyCode = compInfoRepo.GetCompanyCode();

                await repo.Insert(multiComp);

                await auditTrailService.Save(new MultiCompanyDatabase(), multiComp, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(MultiCompanyDatabase multiComp)
        {
            var multiCompDb = await repo.GetByCode(multiComp.DatabaseCode);
            if (multiCompDb != null)
            {
                await repo.Update(multiComp);

                //Audit Trail
                await auditTrailService.Save(multiCompDb, multiComp, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
