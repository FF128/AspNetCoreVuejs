using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.RepositoryInterfaces
{
    public interface ILevelsOfEmployeeRepository
    {
        Task<IEnumerable<LevelsOfEmployee>> GetAll();
        Task<LevelsOfEmployee> GetById(int id);
        Task<LevelsOfEmployee> GetByCode(string code);
        Task Insert(LevelsOfEmployee loe);
        Task Update(LevelsOfEmployee loe);
        Task Delete(int id);
    }
}
