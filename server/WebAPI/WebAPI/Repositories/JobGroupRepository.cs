using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class JobGroupRepository : IJobGroupRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public JobGroupRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_JobGroupSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<JobGroup>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<JobGroup>("sp_JobGroupSetUp_View");
            }
        }

        public async Task<JobGroup> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<JobGroup>("sp_JobGroupSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<JobGroup> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<JobGroup>("sp_JobGroupSetUp_ViewById",
                        new { id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(JobGroup jobGroup)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_JobGroupSetUp_Insert",
                    jobGroup, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(JobGroup jobGroup)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_JobGroupSetUp_Update",
                    jobGroup, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
