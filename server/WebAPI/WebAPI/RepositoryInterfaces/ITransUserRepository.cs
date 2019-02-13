using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.TransUserModel;

namespace WebAPI.RepositoryInterfaces
{
    public interface ITransUserRepository
    {
        Task<int> Insert(TransUser transUser);
        Task InsertTransUserDepartment(IEnumerable<TransUserDepartment> transUserDepartments);
        Task InserTransUserPayLoc(IEnumerable<TransUserPaylocation> transUserPaylocations);
    }
}
