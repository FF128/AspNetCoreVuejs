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
    public class DivisionRepository : IDivisionRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public DivisionRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DivisionSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Division>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Division>("sp_DivisionSetUp_View");
            }
        }

        public async Task<Division> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Division>("sp_DivisionSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Division dep)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DivisionSetUp_Insert",
                    dep, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Division dep)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DivisionSetUp_Update",
                    dep, commandType: CommandType.StoredProcedure);
            }
        }
    }
}