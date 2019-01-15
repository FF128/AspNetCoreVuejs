using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.RepositoryInterfaces
{
    public interface IAreaRepository
    {
        Task<IEnumerable<Area>> GetAll();
        Task<Area> GetById(int id);
        Task Insert(Area area);
        Task Update(Area area);
        Task Delete(int id);
    }

}
