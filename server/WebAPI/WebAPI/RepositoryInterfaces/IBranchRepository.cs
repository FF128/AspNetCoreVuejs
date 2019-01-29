using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Dtos.Branch;

namespace WebAPI.RepositoryInterfaces
{
    public interface IBranchRepository
    {
        Task<IEnumerable<Branch>> GetAll();
        Task<Branch> GetById(int id);
        Task<Branch> GetByCode(string code);
        Task<BranchInsertToPayrollFSDto> GetByCodeFromPayroll(string code, string payrollDB);
        Task<BranchInsertToHRISFSDto> GetByCodeFromHRIS(string code, string hrisDB);
        Task<BranchInsertToTKSFSDto> GetByCodeFromTKS(string code, string tskDB);
        Task Insert(Branch branch);
        Task InsertFileSetup(BranchInsertToFSDto dto);
        Task InsertToPayrollFileSetUp(BranchInsertToPayrollFSDto dto);
        Task InsertToHRISFileSetUp(BranchInsertToHRISFSDto dto);
        Task InsertToTSKFileSetUp(BranchInsertToTKSFSDto dto);
        Task Update(Branch branch);
        Task Delete(int id);
    }
}
