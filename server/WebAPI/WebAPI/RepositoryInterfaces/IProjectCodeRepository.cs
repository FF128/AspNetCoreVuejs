using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.RepositoryInterfaces
{
    public interface IProjectCodeRepository
    {
        Task<IEnumerable<ProjectCodeModel>> GetAll();
        Task<ProjectCodeModel> GetById(int id);
        Task Insert(ProjectCodeModel pc);
        Task Update(ProjectCodeModel pc);
        Task Delete(int id);
    }
}
