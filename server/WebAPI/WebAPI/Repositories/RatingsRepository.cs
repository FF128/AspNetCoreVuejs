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
    public class RatingsRepository : IRatingsRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public RatingsRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_RatingsSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Ratings>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Ratings>("sp_RatingsSetUp_View");
            }
        }

        public async Task<Ratings> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Ratings>("sp_RatingsSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Ratings> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Ratings>("sp_RatingsSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Ratings rate)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_RatingsSetUp_Insert",
                    rate, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Ratings rate)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_RatingsSetUp_Update",
                    rate, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
