using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class ProtectionRepository : IProtectionRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public ProtectionRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task<Protection> GetData(string mode)
        {
            using(var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstAsync<Protection>("usp_GetData",
                        new { Mode = mode }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
