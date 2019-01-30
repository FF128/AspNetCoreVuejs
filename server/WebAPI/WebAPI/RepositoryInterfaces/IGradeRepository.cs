using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.GradeDto;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IGradeRepository
    {
        Task<IEnumerable<Grade>> GetAll();
        Task<Grade> GetById(int id);
        Task<Grade> GetByCode(string code);
        Task<dynamic> GetByCodeFromPayroll(string code, string payrollDB);
        Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB);
        Task<dynamic> GetByCodeFromTKS(string code, string tskDB);
        Task Insert(Grade grade);
        Task InsertFileSetup(Grade grade);
        Task InsertToPayrollFileSetUp(GradeInsertToFileSetupDto dto);
        Task InsertToHRISFileSetUp(GradeInsertToFileSetupDto dto);
        Task InsertToTSKFileSetUp(GradeInsertToFileSetupDto dto);
        Task Update(Grade grade);
        Task Delete(int id);
        Task DeleteByCode(string code);
    }
}
