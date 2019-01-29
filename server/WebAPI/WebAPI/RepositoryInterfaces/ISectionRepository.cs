using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Dtos.Section;

namespace WebAPI.RepositoryInterfaces
{
    public interface ISectionRepository
    {
        Task<IEnumerable<Section>> GetAll();
        Task<Section> GetById(int id);
        Task<Section> GetByCode(string code);
        Task<dynamic> GetByCodeFromPayroll(string code, string payrollDB);
        Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB);
        Task<dynamic> GetByCodeFromTKS(string code, string tskDB);
        Task Insert(Section sec);
        Task InsertFileSetup(Section sec);
        Task InsertToPayrollFileSetUp(SectionInsertToPayrollFSDto dto);
        Task InsertToHRISFileSetUp(SectionInsertToHRISFSDto dto);
        Task InsertToTSKFileSetUp(SectionInsertToTKSFSDto dto);
        Task Update(Section sec);
        Task Delete(int id);
        Task DeleteByCode(string code);
    }
}
