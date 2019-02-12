using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Dtos.BudgetEntry;
using WebAPI.Models;
using WebAPI.Models.BudgetEntryModel;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class BudgetEntryRepository : IBudgetEntryRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public BudgetEntryRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task DeleteByTransNo(string transNo)
        {
            using(var conn = connectionFactory.Connection)
            {
                conn.Open();
                using(var trans = conn.BeginTransaction())
                {
                    try
                    {
                        await conn.ExecuteAsync("sp_BudgetEntryMaintDetails_DeleteByTransNo",
                            new { TransNo = transNo }, trans, commandType: CommandType.StoredProcedure);
                        trans.Commit();
                    }catch
                    {
                        trans.Rollback();
                    }
                    
                }
            }
        }

        public async Task<IEnumerable<BudgetEntryMainHeader>> GetAll()
        {
            using(var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<BudgetEntryMainHeader>("sp_BudgetEntryMainHeader_View", commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<GetAvailableBudgetEntriesDto>> GetBudgetEntriesByBudgetDetailsId(long[] id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<GetAvailableBudgetEntriesDto>("sp_BudgetEntryMaintDetails_GetBudgetEntriesByBudgetDetailsId",
                        new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<GetAvailableBudgetEntriesDto>> GetBudgetEntriesByStatus(string status, string dbName)
        {
            using(var conn = connectionFactory.Connection)
            {
                return 
                    await conn.QueryAsync<GetAvailableBudgetEntriesDto>("sp_BudgetEntryMaintDetails_GetBudgetEntriesByStatus",
                        new { Status = status, DBName = dbName }, commandType: CommandType.StoredProcedure);
            }
        }

        public GetBudgetEntryApprovalDetailsDto GetBudgetEntryApprovalDetailsDtoByTransNo(string transNo)
        {
            using (var conn = connectionFactory.Connection)
            {
                var attachments =  conn.Query<BudgetEntryMaintAttachment>("sp_BudgetEntryMaintAttachment_ViewByTransNo",
                        new { TransNo = transNo },
                        commandType: CommandType.StoredProcedure);
                var beDetails =  conn.Query<GetBudgetEntryMaintDetails>("sp_BudgetEntryMaintDetails_ViewByTransNo",
                        new { TransNo = transNo },
                        commandType: CommandType.StoredProcedure);
                var be =  conn.QueryFirstOrDefault<BudgetEntryMainHeader>("sp_BudgetEntryMainHeader_ViewByTransNo",
                        new { TransNo = transNo },
                        commandType: CommandType.StoredProcedure);

                return new GetBudgetEntryApprovalDetailsDto
                {
                    BudgetEntryMainHeader = be,
                    BudgetEntryMaintAttachments = attachments,
                    BudgetEntryMaintDetails = beDetails
                };

            }
        }
        public async Task<IEnumerable<BudgetEntryMaintAttachment>> GetBudgetEntryMaintAttachmentsByTransNo(string transNo)
        {
            using(var conn = connectionFactory.Connection)
            {
                conn.Open();
                return
                    await conn.QueryAsync<BudgetEntryMaintAttachment>("sp_BudgetEntryMaintAttachment_ViewByTransNo",
                        new { TransNo = transNo },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<GetBudgetEntryMaintDetails>> GetBudgetEntryMaintDetails()
        {
            using(var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<GetBudgetEntryMaintDetails>("sp_BudgetEntryMaintDetails_View", 
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<GetBudgetEntryMaintDetails>> GetBudgetEntryMaintDetailsByTransNo(string transNo, string dbName)
        {
            using (var conn = connectionFactory.Connection)
            {
                conn.Open();
                return
                    await conn.QueryAsync<GetBudgetEntryMaintDetails>("sp_BudgetEntryMaintDetails_ViewByTransNo",
                        new { TransNo = transNo, DBName = dbName },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<BudgetEntryMainHeader> GetByTransNo(string transNo)
        {
            using (var conn = connectionFactory.Connection)
            {
                conn.Open();
                return
                    await conn.QueryFirstOrDefaultAsync<BudgetEntryMainHeader>("sp_BudgetEntryMainHeader_ViewByTransNo",
                        new { TransNo = transNo },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> Insert(BudgetEntryMainHeader budgetEntryMainHeader)
        {
            using(var conn = connectionFactory.Connection)
            {
                conn.Open();
                using(var trans = conn.BeginTransaction())
                {
                    try
                    {
                        var identity = await conn.QueryFirstOrDefaultAsync<int>("sp_BudgetEntryMainHeader_Insert",
                               budgetEntryMainHeader,
                               trans,
                               commandType: CommandType.StoredProcedure);
                        trans.Commit();
                        return identity;
                    }
                    catch
                    {
                        trans.Rollback();
                        return 0;
                    }
                   
                }
                
            }
        }

        public async Task InsertAllocated(IEnumerable<BudgetEntryMaintAllocated> budgetEntryMaintAllocated)
        {
            using(var conn = connectionFactory.Connection)
            {
                conn.Open();
                using(var trans = conn.BeginTransaction())
                {
                    try
                    {
                        await conn.ExecuteAsync("sp_BudgetEntryMaintAllocated_Insert",
                            budgetEntryMaintAllocated, trans, commandType: CommandType.StoredProcedure);

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                }
            }
        }

        public async Task InsertAttachments(IEnumerable<BudgetEntryMaintAttachment> budgetEntryMaintAttachments)
        {
            using (var conn = connectionFactory.Connection)
            {
                
                await conn.ExecuteAsync("sp_BudgetEntryMaintAttachment_Insert",
                    budgetEntryMaintAttachments,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertComment(BudgetEntryMaintReturnComment comment)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_BudgetEntryMaintReturnComment_Insert",
                    comment,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertDetails(IEnumerable<BudgetEntryMaintDetails> budgetEntryMaintDetails)
        {
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_BudgetEntryMaintDetails_Insert", 
                    budgetEntryMaintDetails, 
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(BudgetEntryMainHeader budgetEntryMainHeader)
        {
            using(var conn = connectionFactory.Connection)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        await conn.ExecuteAsync("sp_BudgetEntryMainHeader_Update", budgetEntryMainHeader,
                            transaction: trans, commandType: CommandType.StoredProcedure);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                }
            }
        }

        public async Task UpdateStatus(string transNo, string status)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_BudgetEntryMainHeader_UpdateStatus",
                    new { TransNo = transNo, Status = status },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateTransNo(int id, string transNo)
        {
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_BudgetEntryMainHeader_UpdateTransNo",
                    new { TransactionNo = transNo, ID = id },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
