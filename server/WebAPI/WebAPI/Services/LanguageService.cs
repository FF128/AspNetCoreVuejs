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
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository repo;
        private readonly IAuditTrailService<Language> auditTrailService;
        public LanguageService(ILanguageRepository repo,
             IAuditTrailService<Language> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var language = await repo.GetById(id);
            if (language != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Language(), language, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Language lang)
        {
            if (String.IsNullOrEmpty(lang.LanguageCode) || String.IsNullOrWhiteSpace(lang.LanguageCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(lang.LanguageCode)) == null)
            {
                await repo.Insert(lang);

                await auditTrailService.Save(new Language(), lang, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Language lang)
        {
            var language = await repo.GetByCode(lang.LanguageCode);
            if (language != null)
            {
                await repo.Update(lang);

                //Audit Trail
                await auditTrailService.Save(language, lang, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
