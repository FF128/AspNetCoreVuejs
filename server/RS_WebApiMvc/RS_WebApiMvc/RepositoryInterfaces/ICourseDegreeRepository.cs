using RS_WebApiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
{
    public interface ICourseDegreeRepository
    {
        Task<IEnumerable<CourseDegree>> GetAll();
        Task<CourseDegree> GetById(int id);
        Task Insert(CourseDegree cd);
        Task Update(CourseDegree cd);
        Task Delete(int id);
    }
}
