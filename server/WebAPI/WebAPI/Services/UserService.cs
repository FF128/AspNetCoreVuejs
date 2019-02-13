using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Dtos.UsersDto;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly IUserRepository userRepo;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IMapper mapper;
        public UserService(IConnectionFactory connectionFactory,
            IUserRepository userRepo,
            ICompanyInformationRepository compInfoRepo,
            IMapper mapper)
        {
            this.connectionFactory = connectionFactory;
            this.userRepo = userRepo;
            this.compInfoRepo = compInfoRepo;
            this.mapper = mapper;
        }
        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new AppException("Username or password is null or empty");

            var user = await userRepo.Authenticate(username, password);

            // check if username exists
            if (user == null)
                throw new AppException("User doesn't exist");

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new AppException("Password is incorrect");

            // authentication successful
            return user;
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
        public async Task<CustomMessage> Create(InsertUserDto userDto, string password)
        {
            var user = mapper.Map<User>(userDto.User);

            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (GetUserByUsername(user.Username) != null)
                throw new AppException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;


            user.CompanyCode = compInfoRepo.GetCompanyCode();
            await userRepo.Insert(user);

            foreach (var item in userDto.UserPayLocations)
            {
                item.EmpCode = user.EmpCode;
                item.CompanyCode = user.CompanyCode;
            }
               

            foreach (var item in userDto.UserDepartments)
            {
                item.EmpCode = user.EmpCode;
                item.CompanyCode = user.CompanyCode;
            }


            await userRepo.InsertUserPayLocation(userDto.UserPayLocations);
            await userRepo.InsertUsertDepartment(userDto.UserDepartments);

            return CustomMessageHandler.RecordAdded();
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
