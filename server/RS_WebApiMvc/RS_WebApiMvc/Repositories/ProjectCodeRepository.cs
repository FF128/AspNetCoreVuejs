using Dapper;
using RS_WebApiMvc.Data;
using RS_WebApiMvc.Models;
using RS_WebApiMvc.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RS_WebApiMvc.Repositories
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

        public async Task<IEnumerable<ProjectCode>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<ProjectCode>("sp_ProjectCodeSetUp_View");
            }
        }

        public async Task<ProjectCode> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<ProjectCode>("sp_ProjectCodeSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(ProjectCode pc)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ProjectCodeSetUp_Insert",
                    pc, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(ProjectCode pc)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ProjectCodeSetUp_Update",
                    pc, commandType: CommandType.StoredProcedure);
            }
        }
    }
}