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
    public class AppEntryTextCertService : IAppEntryTextCertService
    {
        private readonly IAppEntryTextCertRepository repo;
        private readonly IAuditTrailService<AppEntryTextCert> auditTrailService;
        private readonly ICompanyInfoService comInfoService;
        public AppEntryTextCertService(IAppEntryTextCertRepository repo,
             IAuditTrailService<AppEntryTextCert> auditTrailService,
             ICompanyInfoService comInfoService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.comInfoService = comInfoService;
        }

        public async Task<CustomMessage> Insert(AppEntryTextCert text)
        {
            var textCertificate = (await repo.GetTextCert());
            if (textCertificate == null)
            {
                await repo.Insert(text);

                await auditTrailService.Save(new AppEntryTextCert(), text, "ADD");

                return CustomMessageHandler.RecordAdded();

            }

            await repo.Update(text);
            //Audit Trail
            await auditTrailService.Save(textCertificate, text, "EDIT");

            return CustomMessageHandler.RecordUpdated();
        }

        public async Task<CustomMessage> Update(AppEntryTextCert text)
        {
            var stData = await repo.GetTextCert();
            if (stData != null)
            {
                await repo.Update(text);

                //Audit Trail
                await auditTrailService.Save(stData, text, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("No data found!");
        }
    }
}
