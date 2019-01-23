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
    public class EmailFormatService : IEmailFormatService
    {
        private readonly IEmailFormatRepository repo;
        private readonly ICompanyInformationRepository compInfoRepo;
        public EmailFormatService(IEmailFormatRepository repo,
            ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.compInfoRepo = compInfoRepo;
        }
        public async Task<CustomMessage> Insert(EmailFormat emailFormat)
        {
            var emailFormatData = await repo.GetByTransType(emailFormat.TransType);
            if(emailFormatData == null)
            {
                emailFormat.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(emailFormat);
                return CustomMessageHandler.RecordUpdated();
            }

            emailFormat.Id = emailFormatData.Id;
            await repo.Update(emailFormat);

            return CustomMessageHandler.RecordUpdated();
        }

        public Task<CustomMessage> Update(EmailFormat emailFormat)
        {
            throw new NotImplementedException();
        }
    }
}
