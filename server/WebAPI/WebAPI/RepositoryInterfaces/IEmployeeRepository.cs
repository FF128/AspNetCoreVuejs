using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployees(int pageSize, int pageNum, string query, string dbName);

        Task<bool> CheckEmpCodeIfExists(string empCode, string dbName);
    }
}
