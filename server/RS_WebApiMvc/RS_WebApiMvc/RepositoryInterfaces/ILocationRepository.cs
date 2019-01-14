using RS_WebApiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAll();
        Task<Location> GetById(int id);
        Task Insert(Location loc);
        Task Update(Location loc);
        Task Delete(int id);
    }
}
