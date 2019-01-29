using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Dtos.Location;

namespace WebAPI.RepositoryInterfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAll();
        Task<Location> GetById(int id);
        Task<Location> GetByCode(string code);
        Task<dynamic> GetByCodeFromPayroll(string code, string payrollDB);
        Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB);
        Task<dynamic> GetByCodeFromTKS(string code, string tskDB);
        Task Insert(Location loc);
        Task InsertFileSetup(Location loc);
        Task InsertToPayrollFileSetUp(LocationInsertFileSetupDto dto);
        Task InsertToHRISFileSetUp(LocationInsertFileSetupDto dto);
        Task InsertToTSKFileSetUp(LocationInsertFileSetupDto dto);
        Task Update(Location loc);
        Task Delete(int id);
        Task DeleteByCode(string code);
    }
}
