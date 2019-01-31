using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Dtos.School;
using WebAPI.Dtos;

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
        Task UpdateFileSetup(School school);
        Task UpdateToHRISFileSetUp(SchoolUpdateToHRISFSDto dto);
        Task Delete(string code);
        Task DeleteFromHRISFileSetUp(DeleteSetUpDto dto);
        Task DeleteFileSetUp(string code);
    }
}
