using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IRatingsRepository
    {
        Task<IEnumerable<Ratings>> GetAll();
        Task<Ratings> GetById(int id);
        Task<Ratings> GetByCode(string code);
        Task Insert(Ratings rate);
        Task Update(Ratings rate);
        Task Delete(int id);
    }
}
