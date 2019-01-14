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
    public class MajorRepository : IMajorRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public MajorRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_MajorSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Major>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Major>("sp_MajorSetUp_View");
            }
        }

        public async Task<Major> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Major>("sp_MajorSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Major major)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_MajorSetUp_Insert",
                    major, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Major major)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_MajorSetUp_Update",
                    major, commandType: CommandType.StoredProcedure);
            }
        }
    }
}