using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.UsersDto;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        Task Register(User user, string password);
        User GetById(int id);
        Task<CustomMessage> Create(InsertUserDto user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }
}
