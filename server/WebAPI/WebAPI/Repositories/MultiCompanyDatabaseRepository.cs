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
    public class MultiCompanyDatabaseRepository : IMultiCompanyDatabaseRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public MultiCompanyDatabaseRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_MultiCompanyDatabaseSetUp_DeleteByCode",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<MultiCompanyDatabase>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<MultiCompanyDatabase>("sp_MultiCompanyDatabaseSetUp_View");
            }
        }

        public async Task<MultiCompanyDatabase> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<MultiCompanyDatabase>("sp_MultiCompanyDatabaseSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<MultiCompanyDatabase> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<MultiCompanyDatabase>("sp_MultiCompanyDatabaseSetUp_ViewById",
                        new { id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(MultiCompanyDatabase affiliations)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_MultiCompanyDatabaseSetUp_Insert",
                    affiliations, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(MultiCompanyDatabase affiliations)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_MultiCompanyDatabaseSetUp_Update",
                    affiliations, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
