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
                await conn.ExecuteAsync("sp_GovExamSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<GovExams>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<GovExams>("sp_GovExamSetUp_View");
            }
        }

        public async Task<GovExams> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<GovExams>("sp_GovExamSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(GovExams ge)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_GovExamSetUp_Insert",
                    ge, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(GovExams ge)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_GovExamSetUp_Update",
                    ge, commandType: CommandType.StoredProcedure);
            }
        }
    }
}