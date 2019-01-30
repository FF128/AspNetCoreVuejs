using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Dtos.CitizenshipDto;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface ICitizenshipRepository
    {
        Task<IEnumerable<Citizenship>> GetAll();
        Task<Citizenship> GetById(int id);
        Task<Citizenship> GetByCode(string code);
        Task<dynamic> GetByCodeFromPayroll(string code, string payrollDB);
        Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB);

        Task InsertFileSetup(Citizenship cit);
        Task Insert(Citizenship cit);
        Task Update(Citizenship cit);
        Task UpdateFileSetup(Citizenship cit);
        Task UpdateToPayrollFileSetUp(CitizenshipUpdateToFileSetUpDto dto);
        Task UpdateToHRISFileSetUp(CitizenshipUpdateToFileSetUpDto dto);
        Task Delete(int id);
        Task DeleteByCode(string code);
        Task InsertToPayrollFileSetUp(CitizenshipInsertToFileSetUpDto dto);
        Task InsertToHRISFileSetUp(CitizenshipInsertToFileSetUpDto dto);
    }
}
