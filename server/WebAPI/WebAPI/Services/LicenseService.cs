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
    public class LicenseService : ILicenseService
    {
        private readonly ILicenseRepository repo;
        private readonly IAuditTrailService<License> auditTrailService;
        public LicenseService(ILicenseRepository repo,
             IAuditTrailService<License> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var license = await repo.GetById(id);
            if (license != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new License(), license, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(License lic)
        {
            if (String.IsNullOrEmpty(lic.LicenseCode) || String.IsNullOrWhiteSpace(lic.LicenseCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(lic.LicenseCode)) == null)
            {
                await repo.Insert(lic);

                await auditTrailService.Save(new License(), lic, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(License lic)
        {
            var license = await repo.GetByCode(lic.LicenseCode);
            if (license != null)
            {
                await repo.Update(lic);

                //Audit Trail
                await auditTrailService.Save(license, lic, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
