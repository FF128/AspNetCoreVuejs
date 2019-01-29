using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.Religion;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IReligionRepository
    {
        Task<IEnumerable<Religion>> GetAll();
        Task<Religion> GetById(int id);
        Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB);
        Task Insert(Religion rel);
        Task InsertFileSetup(Religion rel);
        Task InsertToHRISFileSetUp(ReligionInsertToHRISFSDto dto);
        Task Update(Religion rel);
        Task Delete(int id);
        Task<Religion> GetByCode(string code);
        Task DeleteByCode(string code);
    }
}
