using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Dtos.School;

namespace WebAPI.RepositoryInterfaces
{
    public interface ISchoolRepository
    {
        Task<IEnumerable<School>> GetAll();
        Task<School> GetById(int id);
        Task<School> GetByCode(string code);
        Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB);
        Task Insert(School school);
        Task InsertFileSetup(School school);
        Task InsertToHRISFileSetUp(SchoolInsertToHRISFSDto dto);
        Task Update(School school);
        Task Delete(int id);
        Task DeleteByCode(string code);
    }
}
