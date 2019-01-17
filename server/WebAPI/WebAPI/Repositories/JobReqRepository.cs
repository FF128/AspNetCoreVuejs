using Dapper;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebAPI.Repositories
{
    public class JobReqRepository : IJobReqRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public JobReqRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_JobReqSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<JobReq>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<JobReq>("sp_JobReqSetUp_View");
            }
        }

        public async Task<JobReq> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<JobReq>("sp_JobReqSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<JobReq> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<JobReq>("sp_JobReqSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(JobReq jr)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_JobReqSetUp_Insert",
                    jr, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(JobReq jr)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_JobReqSetUp_Update",
                    jr, commandType: CommandType.StoredProcedure);
            }
        }
    }
}