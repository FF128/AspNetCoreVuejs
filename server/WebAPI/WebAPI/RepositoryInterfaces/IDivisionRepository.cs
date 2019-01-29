using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Dtos.Division;

namespace WebAPI.RepositoryInterfaces
{
    public interface IDivisionRepository
    {
        Task<IEnumerable<Division>> GetAll();
        Task<Division> GetById(int id);
        Task<Division> GetByCode(string code);
        Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB);
        Task<dynamic> GetByCodeFromTKS(string code, string tskDB);
        Task Insert(Division div);
        Task InsertFileSetup(DivInsertFileSetupDto dto);
        Task InsertToHRISFileSetUp(DivInsertToHRISFSDto dto);
        Task InsertToTSKFileSetUp(DivInsertToTKSFSDto dto);
        Task Update(Division div);
        Task Delete(int id);
        Task DeleteByCode(string code);
    }
}
