using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.UsersDto;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IUserRepository
    {
        string GetEmpCode();
        Task<IEnumerable<GetAllUsersDto>> GetAllUsers();
        Task<User> GetByEmpCode(string empCode);
        Task<IEnumerable<GetAllUsersDto>> GetAllUsersByCompanyCode(string companyCode);
        Task Insert(User user);
        Task InsertUsertDepartment(IEnumerable<UserDepartment> userDepartments);
        Task InsertUserPayLocation(IEnumerable<UserPayLocation> userPayLocations);
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<UserDepartment>> GetUserDepartments(string empCode, string companyCode);
        Task<IEnumerable<UserPayLocation>> GetUserPayLocations(string empCode, string companyCode);
    }
}
