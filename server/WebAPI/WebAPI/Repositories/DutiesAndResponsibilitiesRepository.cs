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
    public class DutiesAndResponsibilitiesRepository : IDutiesAndResponsibilitiesRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public DutiesAndResponsibilitiesRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DutiesResponsibilities_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<DutiesAndResponsibilities>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<DutiesAndResponsibilities>("sp_DutiesResponsibilities_View");
            }
        }

        public async Task<DutiesAndResponsibilities> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<DutiesAndResponsibilities>("sp_DutiesResponsibilities_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<DutiesAndResponsibilities> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<DutiesAndResponsibilities>("sp_DutiesResponsibilities_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(DutiesAndResponsibilities dr)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DutiesResponsibilities_Insert",
                    dr, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(DutiesAndResponsibilities dr)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DutiesResponsibilities_Update",
                    dr, commandType: CommandType.StoredProcedure);
            }
        }
    }
}