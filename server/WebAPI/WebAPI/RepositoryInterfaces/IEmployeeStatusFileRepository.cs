using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IEmployeeStatusFileRepository
    {
        Task<IEnumerable<EmployeeStatusFile>> GetAll();
        Task<EmployeeStatusFile> GetById(int id);
        Task<EmployeeStatusFile> GetByCode(string code);
        Task Insert(EmployeeStatusFile employeeStatusFile);
        Task Update(EmployeeStatusFile employeeStatusFile);
        Task Delete(int id);
    }
}
