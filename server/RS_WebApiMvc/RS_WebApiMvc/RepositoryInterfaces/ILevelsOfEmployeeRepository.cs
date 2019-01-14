using RS_WebApiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
{
    public interface ILevelsOfEmployeeRepository
    {
        Task<IEnumerable<LevelsOfEmployee>> GetAll();
        Task<LevelsOfEmployee> GetById(int id);
        Task Insert(LevelsOfEmployee loe);
        Task Update(LevelsOfEmployee loe);
        Task Delete(int id);
    }
}
