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
    public class SchoolRepository : ISchoolRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public SchoolRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_SchoolSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<School>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<School>("sp_SchoolSetUp_View");
            }
        }

        public async Task<School> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<School>("sp_SchoolSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(School sec)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_SchoolSetUp_Insert",
                    sec, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task Update(School sec)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_SchoolSetUp_Update",
                    sec, commandType: CommandType.StoredProcedure);
            }
        }
    }
}