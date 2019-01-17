using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface ICitizenshipRepository
    {
        Task<IEnumerable<Citizenship>> GetAll();
        Task<Citizenship> GetById(int id);
        Task<Citizenship> GetByCode(string code);
        Task Insert(Citizenship cit);
        Task Update(Citizenship cit);
        Task Delete(int id);
        Task DeleteByCode(string code);

    }
}
