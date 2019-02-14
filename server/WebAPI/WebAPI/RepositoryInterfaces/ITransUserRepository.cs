using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.TransUserModel;

namespace WebAPI.RepositoryInterfaces
{
    public interface ITransUserRepository
    {
        Task<IEnumerable<TransUser>> GetAll();
        Task<IEnumerable<TransUser>> GetAllByTrans(string companyCode, string trans);
        Task<bool> GetByEmpCode(string empCode, string companyCode);

        Task<IEnumerable<TransUserPaylocation>> GetTransUserPaylocations(long transID, string companyCode);
        Task<IEnumerable<TransUserDepartment>> GetTransUserDepartments(long transID, string companyCode);
        Task<IEnumerable<dynamic>> GetTransUserDepartmentsByDepCode(string depCode,string trans, string companyCode);
        Task<IEnumerable<dynamic>> GetTransUserPayLocByLocId(int locId,string trans, string companyCode);
        Task<int> Insert(TransUser transUser);
        Task InsertTransUserDepartment(IEnumerable<TransUserDepartment> transUserDepartments);
        Task InserTransUserPayLoc(IEnumerable<TransUserPaylocation> transUserPaylocations);

    }
}
