using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using Dapper;
using System.Data;

namespace WebAPI.Repositories
{
    public class JobCategoryRepository : IJobCategoryRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public JobCategoryRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task Delete(string code)
        {
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_JobCategorySetup_Delete",
                    new { code }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteJobCatDetail(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_JobCategoryDetailSetup_Delete",
                    new { code }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<JobCategory>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
               return 
                    await conn.QueryAsync<JobCategory>("sp_JobCategorySetup_View",
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<JobCategory> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return 
                    await conn.QuerySingleOrDefaultAsync<JobCategory>("sp_JobCategorySetup_ViewByCode", 
                        new { code }, commandType: CommandType.StoredProcedure);
            }
        }

        public Task<JobCategory> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JobCategoryDetail>> GetCategoryDetailsByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<JobCategoryDetail>("sp_JobCategoryDetailSetup_ViewByCode",
                        new { code }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(JobCategory jobGroup)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_JobCategorySetup_Insert",
                    jobGroup, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertJobCatDetail(IEnumerable<JobCategoryDetail> jobCategoryDetails)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_JobCategoryDetailSetup_Insert",
                    jobCategoryDetails, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(JobCategory jobGroup)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_JobCategorySetup_Update",
                    jobGroup, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
