using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IScreenTypeRepository
    {
        Task<IEnumerable<ScreenType>> GetAll();
        Task<ScreenType> GetById(int id);
        Task<ScreenType> GetByCode(string code);
        Task Insert(ScreenType st);
        Task Update(ScreenType st);
        Task Delete(int id);
    }
}
