using Dapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
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
        public UserService(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public User Authenticate(string username, string password)
        {
            using(var conn = connectionFactory.Connection)
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    return null;

                var user = conn.QueryFirstOrDefault<User>("sp_User_GetByUsername",
                    new { Username = username },
                    commandType: CommandType.StoredProcedure);

                // check if username exists
                if (user == null)
                    return null;

                // check if password is correct
                if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    return null;

                // authentication successful
                return user;
            }
        }
        private User GetUserByUsername(string username)
        {
            using(var conn = connectionFactory.Connection)
            {
                var user = conn.QueryFirstOrDefault<User>("sp_User_GetByUsername",
                   new { Username = username },
                   commandType: CommandType.StoredProcedure);
                if (user == null)
                    return null;

                return user;
            }
        }
        public async Task<User> Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (GetUserByUsername(user.Username) != null)
                throw new AppException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            //_context.Users.Add(user);
            //_context.SaveChanges();
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_User_Create", user,commandType: CommandType.StoredProcedure);
            }

            return user;
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public User GetById(int id)
        {
            using(var conn = connectionFactory.Connection)
            {
                return conn.QueryFirstOrDefault<User>("sp_User_GetById",
                    new { UserId = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public Task Register(User user, string password)
        {
            throw new System.NotImplementedException();
        }

        public void Update(User user, string password = null)
        {
            throw new System.NotImplementedException();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
