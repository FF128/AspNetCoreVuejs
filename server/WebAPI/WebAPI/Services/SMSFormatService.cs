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
    public class SMSFormatService : ISMSFormatService
    {
        private readonly ISMSFormatRepository repo;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IAuditTrailService<SMSFormat> auditTrailService;

        public SMSFormatService(ISMSFormatRepository repo,
            ICompanyInformationRepository compInfoRepo,
            IAuditTrailService<SMSFormat> auditTrailService)
        {
            this.repo = repo;
            this.compInfoRepo = compInfoRepo;
            this.auditTrailService = auditTrailService;
        }
        public async Task<CustomMessage> Insert(SMSFormat smsFormat)
        {
            var smsFormatData = await repo.GetByTransType(smsFormat.TransType);
            if (smsFormatData == null)
            {
                smsFormat.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(smsFormat);

                await auditTrailService.Save(new SMSFormat(), smsFormat, "ADD");

                return CustomMessageHandler.RecordUpdated();
            }

            smsFormat.Id = smsFormatData.Id;
            await repo.Update(smsFormat);
            await auditTrailService.Save(smsFormatData, smsFormat, "UPDATE");

            return CustomMessageHandler.RecordUpdated();
        }

        public Task<CustomMessage> Update(SMSFormat smsFormat)
        {
            throw new NotImplementedException();
        }
    }
}
