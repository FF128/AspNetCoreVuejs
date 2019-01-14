using RS_WebApiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
{
    public interface ICitizenshipRepository
    {
        Task<IEnumerable<Citizenship>> GetAll();
        Task<Citizenship> GetById(int id);
        Task Insert(Citizenship cit);
        Task Update(Citizenship cit);
        Task Delete(int id);
    }
}
