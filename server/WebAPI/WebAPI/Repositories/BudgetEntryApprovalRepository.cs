using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Dtos.BudgetEntry;
using WebAPI.Models.BudgetEntryModel;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class BudgetEntryApprovalRepository : IBudgetEntryApprovalRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public BudgetEntryApprovalRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<BudgetEntryMaintDetails>> GetAllByTransNo(string transNo, string dbName)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<BudgetEntryMaintDetails>("sp_BudgetEntryMaintDetails_ViewByTransNo",
                        new { TransNo = transNo, DBName = dbName },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ViewBudgetEntryWithStatusWaitingDto>> GetBudgetEntriesWithStatusWaiting()
        {
            using(var conn = connectionFactory.Connection)
            {
                return await conn.QueryAsync<ViewBudgetEntryWithStatusWaitingDto>("sp_BudgetEntryMainHeader_ViewByStatus",
                    new { Status = "WAITING" },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<BudgetEntryMaintDetails> GetByTransNo(string transNo, string dbName)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<BudgetEntryMaintDetails>("sp_BudgetEntryMaintDetails_ViewByTransNo",
                        new { TransNo = transNo, DBName = dbName },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateBudgetEntryDetailsStatusApproved(string transNo, string status)
        {
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_BudgetEntryMaintDetails_UpdateStatus",
                    new { TransNo = transNo, Status = status },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateBudgetEntryDetailsStatusDeclined(string transNo, string status)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_BudgetEntryMaintDetails_UpdateStatus",
                    new { TransNo = transNo, Status = status },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateBudgetEntryDetailsStatusReturned(BudgetEntryMaintReturnComment comment, string status)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_BudgetEntryMaintDetails_UpdateStatus",
                    new { TransNo = comment.TransactionNo, Status = status },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateRemarks(UpdateRemarksBudgetEntryDetailDto dto)
        {
            using(var conn = connectionFactory.Connection)
            {
                conn.Open();
                using(var trans = conn.BeginTransaction())
                {
                    try
                    {
                        await conn.ExecuteAsync("sp_BudgetEntryMaintDetails_UpdateRemarks",
                            dto, trans, commandType: CommandType.StoredProcedure);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                }
            }
        }
    }
}
