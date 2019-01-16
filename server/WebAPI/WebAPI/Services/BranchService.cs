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
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository repo;
        private readonly IAuditTrailService<Branch> auditTrailService;
        public BranchService(IBranchRepository repo,
             IAuditTrailService<Branch> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var branch = await repo.GetById(id);
            if (branch != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Branch(), branch, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Branch branch)
        {
            if (String.IsNullOrEmpty(branch.BranchCode) || String.IsNullOrWhiteSpace(branch.BranchCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(branch.BranchCode)) == null)
            {
                await repo.Insert(branch);

                await auditTrailService.Save(new Branch(), branch, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Branch branch)
        {
            var branchData = await repo.GetByCode(branch.BranchCode);
            if (branchData != null)
            {
                await repo.Update(branch);

                //Audit Trail
                await auditTrailService.Save(branchData, branch, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
