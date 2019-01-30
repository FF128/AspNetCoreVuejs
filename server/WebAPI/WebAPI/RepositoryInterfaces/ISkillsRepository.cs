using WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Dtos.SkillsDto;

namespace WebAPI.RepositoryInterfaces
{
    public interface ISkillsRepository
    {
        Task<IEnumerable<Skills>> GetAll();
        Task<Skills> GetById(int id);
        Task<Skills> GetByCode(string code);
        Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB);
        Task Insert(Skills sk);
        Task InsertFileSetup(Skills sk);
        Task InsertToHRISFileSetUp(SkillInsertToHRISFSDto dto);
        Task Update(Skills sk);
        Task Delete(int id);
        Task DeleteByCode(string code);
    }
}