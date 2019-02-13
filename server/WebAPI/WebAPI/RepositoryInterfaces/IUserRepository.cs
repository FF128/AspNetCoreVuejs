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
        Task Insert(User user);
        Task InsertUsertDepartment(IEnumerable<UserDepartment> userDepartments);
        Task InsertUserPayLocation(IEnumerable<UserPayLocation> userPayLocations);
        Task<User> Authenticate(string username, string password);
    }
}
