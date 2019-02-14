using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Dtos;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class PayLocationRepository : IPayLocationRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public PayLocationRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<PayLocationDto> GetPayLocationById(long locId, string dbName)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<PayLocationDto>("sp_tbl_fsPayLocation_ViewFromHRISByLocId",
                        new { LocId = locId, DBName = dbName },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PayLocationDto>> GetPayLocations(string dbName)
        {
            using(var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<PayLocationDto>("sp_tbl_fsPayLocation_ViewFromHRIS", 
                        new { DBName = dbName},
                        commandType: CommandType.StoredProcedure);
            }
        }
    }
}
