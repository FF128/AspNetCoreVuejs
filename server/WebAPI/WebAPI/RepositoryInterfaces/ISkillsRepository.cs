using WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.RepositoryInterfaces
{
    public interface ISkillsRepository
    {
        Task<IEnumerable<Skills>> GetAll();
        Task<Skills> GetById(int id);
        Task Insert(Skills sk);
        Task Update(Skills sk);
        Task Delete(int id);
    }
}