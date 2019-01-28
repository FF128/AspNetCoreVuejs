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
    public class JobReqService : IJobReqService
    {
        private readonly IJobReqRepository repo;
        private readonly IAuditTrailService<JobReq> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public JobReqService(IJobReqRepository repo,
             IAuditTrailService<JobReq> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var jobReqData = await repo.GetById(id);
            if (jobReqData != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new JobReq(), jobReqData, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(JobReq jobReq)
        {
            if (String.IsNullOrEmpty(jobReq.JobReqCode) || String.IsNullOrWhiteSpace(jobReq.JobReqCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(jobReq.JobReqCode)) == null)
            {
                jobReq.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(jobReq);

                await auditTrailService.Save(new JobReq(), jobReq, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(JobReq jobReq)
        {
            var jobReqData = await repo.GetByCode(jobReq.JobReqCode);
            if (jobReqData != null)
            {
                await repo.Update(jobReq);

                //Audit Trail
                await auditTrailService.Save(jobReqData, jobReq, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
