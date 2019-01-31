using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Dtos.MajorDto;
using WebAPI.Dtos;

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
        Task UpdateFileSetup(Major major);
        Task UpdateToHRISFileSetUp(MajorUpdateToHRISFSDto dto);
        Task Delete(string code);
        Task DeleteFromHRISFileSetUp(DeleteSetUpDto dto);
        Task DeleteFileSetUp(string code);

    }
}
