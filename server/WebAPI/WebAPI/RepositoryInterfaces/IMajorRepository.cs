using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.RepositoryInterfaces
{
    public interface IMajorRepository
    {
        Task<IEnumerable<Major>> GetAll();
        Task<Major> GetById(int id);
        Task Insert(Major major);
        Task Update(Major major);
        Task Delete(int id);
    }
}
