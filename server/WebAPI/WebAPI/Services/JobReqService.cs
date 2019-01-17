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
    public class JobReqService : IJobReqService
    {
        private readonly IJobReqRepository repo;
        private readonly IAuditTrailService<JobReq> auditTrailService;
        public JobReqService(IJobReqRepository repo,
             IAuditTrailService<JobReq> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var payHouse = await repo.GetById(id);
            if (payHouse != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new JobReq(), payHouse, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(JobReq ph)
        {
            if (String.IsNullOrEmpty(ph.JobReqCode) || String.IsNullOrWhiteSpace(ph.JobReqCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(ph.JobReqCode)) == null)
            {
                await repo.Insert(ph);

                await auditTrailService.Save(new JobReq(), ph, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(JobReq ph)
        {
            var payHouse = await repo.GetByCode(ph.JobReqCode);
            if (payHouse != null)
            {
                await repo.Update(ph);

                //Audit Trail
                await auditTrailService.Save(payHouse, ph, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
