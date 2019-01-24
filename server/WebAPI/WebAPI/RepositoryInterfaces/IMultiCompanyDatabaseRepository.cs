using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IMultiCompanyDatabaseRepository
    {
        Task<IEnumerable<MultiCompanyDatabase>> GetAll();
        Task<MultiCompanyDatabase> GetById(int id);
        Task<MultiCompanyDatabase> GetByCode(string code);
        Task Insert(MultiCompanyDatabase multiCompany);
        Task Update(MultiCompanyDatabase multiCompany);
        Task Delete(string code);
    }
}
