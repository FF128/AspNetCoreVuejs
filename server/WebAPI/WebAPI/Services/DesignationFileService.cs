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
    public class DesignationFileService : IDesignationFileService
    {
        private readonly IDesignationFileRepository repo;
        private readonly IAuditTrailService<DesignationFile> auditTrailService;
        public DesignationFileService(IDesignationFileRepository repo,
             IAuditTrailService<DesignationFile> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var DesignationFile = await repo.GetById(id);
            if (DesignationFile != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new DesignationFile(), DesignationFile, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(DesignationFile df)
        {
            if (String.IsNullOrEmpty(df.Code) || String.IsNullOrWhiteSpace(df.Code))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(df.Code)) == null)
            {
                await repo.Insert(df);

                await auditTrailService.Save(new DesignationFile(), df, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(DesignationFile df)
        {
            var DesignationFileData = await repo.GetByCode(df.Code);
            if (DesignationFileData != null)
            {
                await repo.Update(df);

                //Audit Trail
                await auditTrailService.Save(DesignationFileData, df, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
