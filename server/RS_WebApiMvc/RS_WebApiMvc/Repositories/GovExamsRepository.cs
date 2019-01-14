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
    public class GovExamsRepository : IGovExamsRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public GovExamsRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_GovExamsSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<GovExams>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<GovExams>("sp_GovExamsSetUp_View");
            }
        }

        public async Task<GovExams> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<GovExams>("sp_GovExamsSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(GovExams ge)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_GovExamsSetUp_Insert",
                    ge, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(GovExams ge)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_GovExamsSetUp_Update",
                    ge, commandType: CommandType.StoredProcedure);
            }
        }
    }
}