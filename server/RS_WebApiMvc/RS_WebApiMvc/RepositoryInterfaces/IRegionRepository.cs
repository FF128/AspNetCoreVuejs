using RS_WebApiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAll();
        Task<Region> GetById(int id);
        Task Insert(Region reg);
        Task Update(Region reg);
        Task Delete(int id);
    }
}
