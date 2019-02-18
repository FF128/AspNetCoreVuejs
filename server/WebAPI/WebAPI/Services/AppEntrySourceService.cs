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
    public class AppEntrySourceService : IAppEntrySourceService
    {
        private readonly IAppEntrySourceRepository repo;
        private readonly IAuditTrailService<AppEntrySource> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IUserRepository userRepo;
        public AppEntrySourceService(IAppEntrySourceRepository repo,
             IAuditTrailService<AppEntrySource> auditTrailService,
             ICompanyInformationRepository compInfoRepo,
             IUserRepository userRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
            this.userRepo = userRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var source = await repo.GetById(id);
            if (source != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new AppEntrySource(), source, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(AppEntrySource source)
        {
            if ((await repo.GetById(source.Id)) == null)
            {
                source.CompanyCode = compInfoRepo.GetCompanyCode();
                source.CreatedBy = userRepo.GetEmpCode();
                await repo.Insert(source);

                await auditTrailService.Save(new AppEntrySource(), source, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(AppEntrySource source)
        {
            var sourceData = await repo.GetById(source.Id);
            if (sourceData != null)
            {
                source.EditedBy = userRepo.GetEmpCode();
                await repo.Update(source);

                //Audit Trail
                await auditTrailService.Save(sourceData, source, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("Data not found");
        }
    }
}
