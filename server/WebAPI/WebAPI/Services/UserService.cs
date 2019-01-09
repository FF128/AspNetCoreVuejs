using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {

        private readonly IConnectionFactory connectionFactory;
        private readonly AppSettings appSettings;
        private readonly PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        public UserService(IConnectionFactory connectionFactory, 
            AppSettings appSettings)
        {
            this.connectionFactory = connectionFactory;
            this.appSettings = appSettings;
        }
        public User Authenticate(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task Register(User user, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}
