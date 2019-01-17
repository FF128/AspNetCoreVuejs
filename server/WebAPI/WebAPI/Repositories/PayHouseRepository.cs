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

        public async Task<PayHouse> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<PayHouse>("sp_PayHouseSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
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