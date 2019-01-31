using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Dtos.Location;
using WebAPI.Dtos;

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
        Task InsertToTKSFileSetUp(LocationInsertFileSetupDto dto);
        Task Update(Location loc);
        Task UpdateFileSetup(Location loc);
        Task UpdateToPayrollFileSetUp(LocationUpdateFileSetupDto dto);
        Task UpdateToHRISFileSetUp(LocationUpdateFileSetupDto dto);
        Task UpdateToTKSFileSetUp(LocationUpdateFileSetupDto dto);
        Task Delete(string code);
        Task DeleteFromHRISFileSetUp(DeleteSetUpDto dto);
        Task DeleteFromPayrollFileSetUp(DeleteSetUpDto dto);
        Task DeleteFromTKSFileSetUp(DeleteSetUpDto dto);
        Task DeleteFileSetUp(string code);
    }
}
