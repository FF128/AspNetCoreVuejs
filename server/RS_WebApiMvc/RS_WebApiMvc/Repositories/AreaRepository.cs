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
    public class AreaRepository : IAreaRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public AreaRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AreaSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Area>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Area>("sp_AreaSetUp_View");
            }
        }

        public async Task<Area> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Area>("sp_AreaSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Area area)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AreaSetUp_Insert",
                    area, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Area area)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AreaSetUp_Update",
                    area, commandType: CommandType.StoredProcedure);
            }
        }
    }
}