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
    public class UnitRepository : IUnitRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public UnitRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_UnitSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Unit>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Unit>("sp_UnitSetUp_View");
            }
        }

        public async Task<Unit> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Unit>("sp_UnitSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Unit dep)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_UnitSetUp_Insert",
                    dep, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Unit dep)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_UnitSetUp_Update",
                    dep, commandType: CommandType.StoredProcedure);
            }
        }
    }
}