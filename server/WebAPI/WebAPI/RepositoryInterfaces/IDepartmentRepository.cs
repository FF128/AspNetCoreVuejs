using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Dtos.Department;

namespace WebAPI.RepositoryInterfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAll();
        Task<Department> GetById(int id);
        Task<Department> GetByCode(string code);
        Task<dynamic> GetByCodeFromPayroll(string code, string payrollDB);
        Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB);
        Task<dynamic> GetByCodeFromTKS(string code, string tskDB);
        Task Insert(Department dep);
        Task InsertFileSetup(Department dep);
        Task InsertToPayrollFileSetUp(DepartmentInsertToPayrollFSDto dto);
        Task InsertToHRISFileSetUp(DepartmentInsertToHRISFSDto dto);
        Task InsertToTSKFileSetUp(DepartmentInsertToTKSFSDto dto);
        Task Update(Department dep);
        Task Delete(int id);
        Task DeleteByCode(string code);
    }
}
