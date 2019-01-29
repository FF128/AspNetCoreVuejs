using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Dtos;

namespace WebAPI.RepositoryInterfaces
{
    public interface IAreaRepository
    {
        Task<IEnumerable<Area>> GetAll();
        Task<Area> GetById(int id);
        Task<Area> GetByCode(string code);
        Task<dynamic> GetByCodeFromPayroll(string code, string payrollDB);
        Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB);
        Task<dynamic> GetByCodeFromTKS(string code, string tskDB);
        Task Insert(Area area);
        Task InsertFileSetup(Area area);
        Task InsertToPayrollFileSetUp(AreaInsertToFileSetUpDto dto);
        Task InsertToHRISFileSetUp(AreaInsertToFileSetUpDto dto);
        Task InsertToTSKFileSetUp(AreaInsertToFileSetUpDto dto);
        Task Update(Area area);
        Task Delete(int id);
        Task DeleteByCode(string code);
    }

}
