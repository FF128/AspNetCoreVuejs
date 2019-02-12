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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public EmployeeRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task<IEnumerable<Employee>> GetAllEmployees(int pageSize, int pageNum, string query, string dbName)
        {
            using(var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Employee>("sp_EmpMasterFile_Pagination",
                        new { PageSize = pageSize, PageNumber = pageNum, Query = query, DBName = dbName },
                        commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
