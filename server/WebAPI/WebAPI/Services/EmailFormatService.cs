﻿using System;
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
        private readonly IAuditTrailService<EmailFormat> auditTrailService;
        public EmailFormatService(IEmailFormatRepository repo,
            ICompanyInformationRepository compInfoRepo,
            IAuditTrailService<EmailFormat> auditTrailService)
        {
            this.repo = repo;
            this.compInfoRepo = compInfoRepo;
            this.auditTrailService = auditTrailService;
        }
        public async Task<CustomMessage> Insert(EmailFormat emailFormat)
        {
            var emailFormatData = await repo.GetByTransType(emailFormat.TransType);
            if(emailFormatData == null)
            {
                emailFormat.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(emailFormat);

                await auditTrailService.Save(new EmailFormat(), emailFormat, "ADD");

                return CustomMessageHandler.RecordUpdated();
            }

            emailFormat.Id = emailFormatData.Id;
            await repo.Update(emailFormat);
            await auditTrailService.Save(emailFormatData, emailFormat, "UPDATE");

            return CustomMessageHandler.RecordUpdated();
        }

        public Task<CustomMessage> Update(EmailFormat emailFormat)
        {
            throw new NotImplementedException();
        }
    }
}
