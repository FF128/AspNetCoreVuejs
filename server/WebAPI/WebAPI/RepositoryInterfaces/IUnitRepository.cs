using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Dtos.Unit;

namespace WebAPI.RepositoryInterfaces
{
    public interface IUnitRepository
    {
        Task<IEnumerable<Unit>> GetAll();
        Task<Unit> GetById(int id);
        Task<Unit> GetByCode(string code);
        Task<dynamic> GetByCodeFromPayroll(string code, string payrollDB);
        Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB);
        Task Insert(Unit unit);
        Task InsertFileSetup(Unit unit);
        Task InsertToPayrollFileSetUp(UnitInsertToPayrollFSDto dto);
        Task InsertToHRISFileSetUp(UnitInsertToHRISFSDto dto);
        Task Update(Unit unit);
        Task Delete(int id);
        Task DeleteByCode(string code);
    }
}
