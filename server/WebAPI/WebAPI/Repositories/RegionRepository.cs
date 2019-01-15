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
    public class RegionRepository : IRegionRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public RegionRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_RegionSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Region>("sp_RegionSetUp_View");
            }
        }

        public async Task<Region> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Region>("sp_RegionSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Region reg)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_RegionSetUp_Insert",
                    reg, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Region reg)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_RegionSetUp_Update",
                    reg, commandType: CommandType.StoredProcedure);
            }
        }
    }
}