using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Dtos.CourseDto;

namespace WebAPI.RepositoryInterfaces
{
    public interface ICourseDegreeRepository
    {
        Task<IEnumerable<CourseDegree>> GetAll();
        Task<CourseDegree> GetById(int id);
        Task<CourseDegree> GetByCode(string code);
        Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB);
        Task Insert(CourseDegree cd);
        Task InsertFileSetup(CourseDegree cd);
        Task InsertToHRISFileSetUp(CourseInsertToHRISFSDto dto);
        Task Update(CourseDegree cd);
        Task UpdateFileSetup(CourseDegree cd);
        Task UpdateToHRISFileSetUp(CourseUpdateToHRISFSDto dto);
        Task Delete(int id);
        Task DeleteByCode(string code);
    }
}
