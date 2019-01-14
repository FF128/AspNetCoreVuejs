using RS_WebApiMvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
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