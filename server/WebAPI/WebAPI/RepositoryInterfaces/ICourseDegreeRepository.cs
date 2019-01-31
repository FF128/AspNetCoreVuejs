using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Dtos.CourseDto;
using WebAPI.Dtos;

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
        Task Delete(string code);
        Task DeleteFromHRISFileSetUp(DeleteSetUpDto dto);
        Task DeleteFileSetUp(string code);
    }
}
