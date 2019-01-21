using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IOverallRatingsRepository
    {
        Task<IEnumerable<OverallRatings>> GetAll();
        Task<OverallRatings> GetById(int id);
        Task<OverallRatings> GetByCode(string code);
        Task Insert(OverallRatings st);
        Task Update(OverallRatings st);
        Task Delete(int id);
    }
}
