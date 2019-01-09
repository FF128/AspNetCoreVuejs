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
    public class JobLevelRepository : IJobLevelRepository
    {
        private readonly IConnectionFactory connectionFactory;

        public JobLevelRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_JobLevel_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<JobLevel>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<JobLevel>("sp_JobLevel_View");
            }
        }

        public async Task<JobLevel> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<JobLevel>("sp_JobLevel_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(JobLevel jobLevel)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_JobLevel_Insert",
                    jobLevel, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(JobLevel jobLevel)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_JobLevel_Update",
                    jobLevel, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
