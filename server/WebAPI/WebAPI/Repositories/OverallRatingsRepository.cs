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
    public class OverallRatingsRepository : IOverallRatingsRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public OverallRatingsRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_OverallRatingsSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<OverallRatings>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<OverallRatings>("sp_OverallRatingsSetUp_View");
            }
        }

        public async Task<OverallRatings> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<OverallRatings>("sp_OverallRatingsSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<OverallRatings> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<OverallRatings>("sp_OverallRatingsSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(OverallRatings rate)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_OverallRatingsSetUp_Insert",
                    rate, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(OverallRatings rate)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_OverallRatingsSetUp_Update",
                    rate, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
