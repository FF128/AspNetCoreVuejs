using RS_WebApiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
{
    public interface IUnitRepository
    {
        Task<IEnumerable<Unit>> GetAll();
        Task<Unit> GetById(int id);
        Task Insert(Unit unit);
        Task Update(Unit unit);
        Task Delete(int id);
    }
}
