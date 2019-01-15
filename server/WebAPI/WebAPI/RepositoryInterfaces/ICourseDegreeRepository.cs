using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.RepositoryInterfaces
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
