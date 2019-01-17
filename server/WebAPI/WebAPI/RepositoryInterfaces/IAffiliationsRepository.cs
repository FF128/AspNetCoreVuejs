using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IAffiliationsRepository
    {
        Task<IEnumerable<Affiliations>> GetAll();
        Task<Affiliations> GetById(int id);
        Task<Affiliations> GetByCode(string code);
        Task Insert(Affiliations affiliations);
        Task Update(Affiliations affiliations);
        Task Delete(int id);
    }
}
