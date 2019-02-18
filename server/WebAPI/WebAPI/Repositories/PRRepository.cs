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

        public async Task<PRFDetailsMaint> GetDetailById(long id)
        {
            using(var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<PRFDetailsMaint>("sp_PRFDetailsMaint_ViewById",
                        new { ID = id }, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<GetExtendPRFDetails>> GetExtendPRFDetails(string companyCode)
        {
            using(var conn = connectionFactory.Connection)
            {
                return await conn.QueryAsync<GetExtendPRFDetails>("sp_PRFExtend_ViewTransactions",
                    new { CompanyCode = companyCode }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<GetExtendPRFDetails> GetExtendPRFDetailsByPRFNo(string prfNo, string companyCode)
        {
            using (var conn = connectionFactory.Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<GetExtendPRFDetails>("sp_PRFExtend_ViewTransactionsByPRFNo",
                    new { PRFNo = prfNo, CompanyCode = companyCode }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<GetPREntryMaintDetails>> GetPREntryMaintDetailsByPRFNo(string prfNo, string dbName)
        {
            using(var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<GetPREntryMaintDetails>("sp_PRFDetailsMaint_ViewByPRFNo");
            }
        }

        public async  Task<PRFExtend> GetPRFExtendById(int id, string companyCode)
        {
            using(var conn = connectionFactory.Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<PRFExtend>("sp_PRFExtend_ViewById",
                    new { ID = id, CompanyCode = companyCode }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PRFExtend>> GetPRFExtendByStatus(string status, string companyCode)
        {
            using (var conn = connectionFactory.Connection)
            {
                return await conn.QueryAsync<PRFExtend>("sp_PRFExtend_ViewByStatus",
                    new { Status = status, CompanyCode = companyCode }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PRFMainReturnComment>> GetPRFHeaderMaintReturnCommentsByPRFNo(string prfNo)
        {
            using(var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<PRFMainReturnComment>("sp_PRFMainReturnComment_GetByPRFNo",
                        new { PRFNo = prfNo }, commandType: CommandType.StoredProcedure);
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

        public async Task InsertComment(PRFMainReturnCommentDto comment)
        {
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_PRFMainReturnComment_Insert", 
                    comment, commandType: CommandType.StoredProcedure);
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

        public async Task InsertDetails(PRFDetailsMaint detail)
        {
            using (var conn = connectionFactory.Connection)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        await conn.ExecuteAsync("sp_PRFDetailsMaint_Insert", detail,
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

        public async Task InsertPRFExtend(PRFExtend prfExtend)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_PRFExtend_Insert",
                    prfExtend, commandType: CommandType.StoredProcedure);
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

        public async Task PRFExtendApproveDeclined(int id, string status, string empCode)
        {
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_PRFExtend_ApprovedDeclined",
                    new { ID = id, Status = status, EmpCode = empCode }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(PRFHeaderMaint header)
        {
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_PRFHeaderMaint_Update",
                    header, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateDetails(PRFDetailsMaint detail)
        {
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_PRFDetailsMaint_Update",
                    detail, commandType: CommandType.StoredProcedure); 
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

        public async Task UpdateTransApprovingStatus(UpdatePRFTransApprovingStatusDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_TransApprovingPRF_UpdateStatus",
                    dto, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<ViewPREntryWithStatusDto>> ViewPRFEntryByStatus(string status)
        {
            using(var conn = connectionFactory.Connection)
            {
                return await conn.QueryAsync<ViewPREntryWithStatusDto>("sp_PRFHeaderMaint_ViewByStatus",
                    new { Status = status }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
