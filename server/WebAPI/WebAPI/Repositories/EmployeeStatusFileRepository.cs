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
    public class EmployeeStatusFileRepository : IEmployeeStatusFileRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public EmployeeStatusFileRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_EmployeeStatusFile_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<EmployeeStatusFile>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<EmployeeStatusFile>("sp_EmployeeStatusFile_View");
            }
        }

        public async Task<EmployeeStatusFile> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<EmployeeStatusFile>("sp_EmployeeStatusFile_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(EmployeeStatusFile employeeStatus)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_EmployeeStatusFile_Insert",
                    employeeStatus, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(EmployeeStatusFile employeeStatus)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_EmployeeStatusFile_Update",
                    employeeStatus, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
