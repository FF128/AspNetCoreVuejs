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
    public class LevelsOfEmployeeRepository : ILevelsOfEmployeeRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public LevelsOfEmployeeRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_LevelsOfEmployeeSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<LevelsOfEmployee>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<LevelsOfEmployee>("sp_LevelsOfEmployeeSetUp_View");
            }
        }

        public async Task<LevelsOfEmployee> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<LevelsOfEmployee>("sp_LevelsOfEmployeeSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(LevelsOfEmployee cit)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_LevelsOfEmployeeSetUp_Insert",
                    cit, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(LevelsOfEmployee cit)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_LevelsOfEmployeeSetUp_Update",
                    cit, commandType: CommandType.StoredProcedure);
            }
        }
    }
}