using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models.ApplicantsEntrySetUp;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class AppEntryGenInfoService : IAppEntryGenInfoService
    {
        private readonly IAppEntryGenInfoRepository repo;
        private readonly IAuditTrailService<AppEntryGenInfo> auditTrailService;
        public AppEntryGenInfoService(IAppEntryGenInfoRepository repo,
             IAuditTrailService<AppEntryGenInfo> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var gen = await repo.GetById(id);
            if (gen != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new AppEntryGenInfo(), gen, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(AppEntryGenInfo gen)
        {
            if ((await repo.GetById(gen.Id)) == null)
            {
                await repo.Insert(gen);

                await auditTrailService.Save(new AppEntryGenInfo(), gen, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(AppEntryGenInfo gen)
        {
            var genData = await repo.GetById(gen.Id);
            if (genData != null)
            {
                await repo.Update(gen);

                //Audit Trail
                await auditTrailService.Save(genData, gen, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
