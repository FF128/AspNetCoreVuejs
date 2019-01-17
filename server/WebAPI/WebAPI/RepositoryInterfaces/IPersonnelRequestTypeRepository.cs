using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IPersonnelRequestTypeRepository
    {
        Task<IEnumerable<PersonnelRequestType>> GetAll();
        Task<PersonnelRequestType> GetById(int id);
        Task<PersonnelRequestType> GetByCode(string code);
        Task Insert(PersonnelRequestType prt);
        Task Update(PersonnelRequestType prt);
        Task Delete(int id);
    }
}
