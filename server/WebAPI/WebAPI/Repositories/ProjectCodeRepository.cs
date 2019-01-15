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
    public class ProjectCodeRepository : IProjectCodeRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public ProjectCodeRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ProjectCodeSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ProjectCodeModel>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<ProjectCodeModel>("sp_ProjectCodeSetUp_View");
            }
        }

        public async Task<ProjectCodeModel> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<ProjectCodeModel>("sp_ProjectCodeSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(ProjectCodeModel pc)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ProjectCodeSetUp_Insert",
                    pc, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(ProjectCodeModel pc)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ProjectCodeSetUp_Update",
                    pc, commandType: CommandType.StoredProcedure);
            }
        }
    }
}