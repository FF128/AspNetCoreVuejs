using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models.TransUserModel;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class TransUserRepository : ITransUserRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public TransUserRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task<int> Insert(TransUser transUser)
        {
            using(var conn = connectionFactory.Connection)
            {
                var identity = await conn.ExecuteAsync("sp_TransUsers_Insert", transUser, commandType: CommandType.StoredProcedure);
                return identity;
            }
        }

        public async Task InserTransUserPayLoc(IEnumerable<TransUserPaylocation> transUserPaylocations)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_TransUsersPaylocation_Insert", transUserPaylocations, commandType: CommandType.StoredProcedure);
                
            }
        }

        public async Task InsertTransUserDepartment(IEnumerable<TransUserDepartment> transUserDepartments)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_TransUsersDepartment_Insert", transUserDepartments, commandType: CommandType.StoredProcedure);

            }
        }
    }
}
