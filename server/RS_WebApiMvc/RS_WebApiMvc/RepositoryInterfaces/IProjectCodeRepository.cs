using RS_WebApiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
{
    public interface IProjectCodeRepository
    {
        Task<IEnumerable<ProjectCode>> GetAll();
        Task<ProjectCode> GetById(int id);
        Task Insert(ProjectCode pc);
        Task Update(ProjectCode pc);
        Task Delete(int id);
    }
}
