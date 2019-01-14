using RS_WebApiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
{
    public interface ISchoolRepository
    {
        Task<IEnumerable<School>> GetAll();
        Task<School> GetById(int id);
        Task Insert(School school);
        Task Update(School school);
        Task Delete(int id);
    }
}
