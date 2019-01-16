using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.RepositoryInterfaces
{
    public interface IRankRepository
    {
        Task<IEnumerable<Rank>> GetAll();
        Task<Rank> GetById(int id);
        Task<Rank> GetByCode(string code);
        Task Insert(Rank rank);
        Task Update(Rank rank);
        Task Delete(int id);
    }
}
