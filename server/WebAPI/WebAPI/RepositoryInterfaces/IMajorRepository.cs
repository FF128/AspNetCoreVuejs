using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Dtos.MajorDto;

namespace WebAPI.RepositoryInterfaces
{
    public interface IMajorRepository
    {
        Task<IEnumerable<Major>> GetAll();
        Task<Major> GetById(int id);
        Task<Major> GetByCode(string code);
        Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB);
        Task Insert(Major major);
        Task InsertFileSetup(Major major);
        Task InsertToHRISFileSetUp(MajorInsertToHRISFSDto dto);
        Task Update(Major major);
        Task Delete(int id);
        Task DeleteByCode(string code);

    }
}
