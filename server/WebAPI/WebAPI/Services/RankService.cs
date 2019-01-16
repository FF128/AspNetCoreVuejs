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
    public class RankService : IRankService
    {
        private readonly IRankRepository repo;
        private readonly IAuditTrailService<Rank> auditTrailService;
        public RankService(IRankRepository repo,
             IAuditTrailService<Rank> auditTrailService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var rank = await repo.GetById(id);
            if (rank != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Rank(), rank, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Rank rank)
        {
            if (String.IsNullOrEmpty(rank.RankCode) || String.IsNullOrWhiteSpace(rank.RankDesc))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(rank.RankCode)) == null)
            {
                await repo.Insert(rank);

                await auditTrailService.Save(new Rank(), rank, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Rank rank)
        {
            var rankData = await repo.GetByCode(rank.RankCode);
            if (rankData != null)
            {
                await repo.Update(rank);

                //Audit Trail
                await auditTrailService.Save(rankData, rank, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
