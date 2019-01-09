using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IReligionRepository
    {
        Task<IEnumerable<Religion>> GetAll();
        Task<Religion> GetById(int id);
        Task Insert(Religion rel);
        Task Update(Religion rel);
        Task Delete(int id);
    }
}
