using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface ICompanyInformationRepository
    {
        Task<IEnumerable<CompanyInfoDto>> GetAll();
        Task<CompanyInfoDto> GetById(int id);
        Task<CompanyInfoDto> GetByCompanyCode(string code);
        Task<bool> CheckDBIfExists(string dbName);
        Task<bool> CheckTableIfExists(string dbName, string tableName);
        Task Insert(CompanyInfoDto info);
        Task Update(CompanyInfoDto info);
        Task Delete(int id);
        string GetCompanyCode();
    }
}
