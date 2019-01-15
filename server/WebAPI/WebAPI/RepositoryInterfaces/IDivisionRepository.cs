using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.RepositoryInterfaces
{
    public interface IDivisionRepository
    {
        Task<IEnumerable<Division>> GetAll();
        Task<Division> GetById(int id);
        Task Insert(Division div);
        Task Update(Division div);
        Task Delete(int id);
    }
}
