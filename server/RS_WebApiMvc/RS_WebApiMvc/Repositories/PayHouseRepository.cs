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
    public class PayHouseRepository : IPayHouseRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public PayHouseRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_PayHouseSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PayHouse>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<PayHouse>("sp_PayHouseSetUp_View");
            }
        }

        public async Task<PayHouse> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<PayHouse>("sp_PayHouseSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(PayHouse ph)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_PayHouseSetUp_Insert",
                    ph, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(PayHouse ph)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_PayHouseSetUp_Update",
                    ph, commandType: CommandType.StoredProcedure);
            }
        }
    }
}