using RS_WebApiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
{
    public interface IRankRepository
    {
        Task<IEnumerable<Rank>> GetAll();
        Task<Rank> GetById(int id);
        Task Insert(Rank rank);
        Task Update(Rank rank);
        Task Delete(int id);
    }
}
