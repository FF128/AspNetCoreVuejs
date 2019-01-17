using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.RepositoryInterfaces
{
    public interface IDutiesAndResponsibilitiesRepository
    {
        Task<IEnumerable<DutiesAndResponsibilities>> GetAll();
        Task<DutiesAndResponsibilities> GetById(int id);
        Task<DutiesAndResponsibilities> GetByCode(string code);
        Task Insert(DutiesAndResponsibilities dr);
        Task Update(DutiesAndResponsibilities dr);
        Task Delete(int id);
    }
}
