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
    public class EvalEmailFormatService : IEvalEmailFormatService
    {
        private readonly IEvalEmailFormatRepository repo;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IAuditTrailService<EvalEmailFormat> auditTrailService;
        public EvalEmailFormatService(IEvalEmailFormatRepository repo,
            ICompanyInformationRepository compInfoRepo,
            IAuditTrailService<EvalEmailFormat> auditTrailService)
        {
            this.repo = repo;
            this.compInfoRepo = compInfoRepo;
            this.auditTrailService = auditTrailService;
        }
        public async Task<CustomMessage> Insert(EvalEmailFormat emailFormat)
        {
            var emailFormatData = await repo.GetByTransType(emailFormat.TransType);
            if (emailFormatData == null)
            {
                emailFormat.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(emailFormat);

                await auditTrailService.Save(new EvalEmailFormat(), emailFormat, "ADD");
                return CustomMessageHandler.RecordUpdated();
            }

            emailFormat.Id = emailFormatData.Id;
            await repo.Update(emailFormat);
            await auditTrailService.Save(emailFormatData, emailFormat, "UPDATE");
            return CustomMessageHandler.RecordUpdated();
        }

        public Task<CustomMessage> Update(EvalEmailFormat emailFormat)
        {
            throw new NotImplementedException();
        }
    }
}
