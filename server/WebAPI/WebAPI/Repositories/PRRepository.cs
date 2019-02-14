using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Dtos.PRDto;
using WebAPI.Models.PRModel;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class PRRepository : IPRRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public PRRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<PRFHeaderMaint> GetByPRFNo(string prfNo)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<PRFHeaderMaint>("sp_PRFHeaderMaint_ViewByPRFNo",
                        new { PRFNo = prfNo }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<GetPREntryDetails>> GetDetailsByPRFNo(string prfNo, string dbName)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<GetPREntryDetails>("sp_PRFDetailsMaint_ViewByPRFNo",
                        new { PRFNo = prfNo, DBName = dbName }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> Insert(PRFHeaderMaint pr)
        {
            using(var conn = connectionFactory.Connection)
            {
                conn.Open();
                using(var trans = conn.BeginTransaction())
                {
                    try
                    {
                        var identity = await conn.QueryFirstOrDefaultAsync<int>("sp_PRFHeaderMaint_Insert", pr,
                                            trans, commandType: CommandType.StoredProcedure);
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

        public async Task InsertAttachments(IEnumerable<PRFDetailsMaintAttachmentDto> attachments)
        {
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_PRFDetailsMaintAttachment_Insert", 
                    attachments, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertDetails(IEnumerable<PRFDetailsMaint> details)
        {
            using (var conn = connectionFactory.Connection)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        await conn.ExecuteAsync("sp_PRFDetailsMaint_Insert", details,
                            trans, commandType: CommandType.StoredProcedure);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                }
            }
        }

        public async Task InsertTransApproving(IEnumerable<TransApprovingPRF> transApprovingPRF)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_TransApprovingPRF_Insert",
                    transApprovingPRF, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdatePRDetailsStatus(string prfNo, string status)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_PRFDetailsMaint_UpdateStatus",
                    new { PRFNo = prfNo, Status = status },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdatePRFNo(int identity, string prfNo)
        {
            using (var conn = connectionFactory.Connection)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        await conn.ExecuteAsync("sp_PRFHeaderMaint_UpdatePRFNo", new { PRFNo = prfNo, ID = identity },
                            trans, commandType: CommandType.StoredProcedure);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                }
            }
        }

        public async Task UpdateStatus(string prfNo, string status)
        {
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_PRFHeaderMaint_UpdateStatus",
                    new { PRFNo = prfNo, Status = status },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ViewPREntryWithStatusDto>> ViewPREntryWithStatusReturnedDto()
        {
            using (var conn = connectionFactory.Connection)
            {
                return await conn.QueryAsync<ViewPREntryWithStatusDto>("sp_PRFHeaderMaint_ViewWithStatusReturned",
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ViewPREntryWithStatusDto>> ViewPREntryWithStatusWaitingDto()
        {
            using (var conn = connectionFactory.Connection)
            {
                return await conn.QueryAsync<ViewPREntryWithStatusDto>("sp_PRFHeaderMaint_ViewWithStatusWaiting",
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
