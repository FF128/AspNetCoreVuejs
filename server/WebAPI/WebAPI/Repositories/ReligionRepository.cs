using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class ReligionRepository : IReligionRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public ReligionRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_Religion_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Religion>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Religion>("sp_Religion_View");
            }
        }

        public async Task<Religion> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Religion>("sp_Religion_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Religion rel)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_Religion_Insert",
                    rel, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Religion rel)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_Religion_Update",
                    rel, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
