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
    public class AppEntryEssayInfoService : IAppEntryEssayInfoService
    {
        private readonly IAppEntryEssayInfoRepository repo;
        private readonly IAuditTrailService<AppEntryEssayInfo> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public AppEntryEssayInfoService(IAppEntryEssayInfoRepository repo,
             IAuditTrailService<AppEntryEssayInfo> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var essay = await repo.GetById(id);
            if (essay != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new AppEntryEssayInfo(), essay, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(AppEntryEssayInfo essay)
        {
            if ((await repo.GetById(essay.Id)) == null)
            {
                essay.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(essay);

                await auditTrailService.Save(new AppEntryEssayInfo(), essay, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(AppEntryEssayInfo essay)
        {
            var essayData = await repo.GetById(essay.Id);
            if (essayData != null)
            {
                await repo.Update(essay);

                //Audit Trail
                await auditTrailService.Save(essayData, essay, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("Data not found");
        }
    }
}
