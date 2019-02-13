using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Dtos.UsersDto;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserRepository(IConnectionFactory connectionFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            this.connectionFactory = connectionFactory;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            using(var conn = connectionFactory.Connection)
            {
                var user = await conn.QueryFirstOrDefaultAsync<User>("sp_User_GetByUsername",
                   new { Username = username },
                   commandType: CommandType.StoredProcedure);

                return user;
            }
        }

        public async Task<IEnumerable<GetAllUsersDto>> GetAllUsers()
        {
            using(var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<GetAllUsersDto>("sp_User_GetAll", commandType: CommandType.StoredProcedure);
            }
        }

        public string GetEmpCode()
        {
            return httpContextAccessor.HttpContext.User.FindFirst("EmpCode")?.Value;
        }

        public async Task Insert(User user)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_User_Create", user, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertUserPayLocation(IEnumerable<UserPayLocation> userPayLocations)
        {
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_UserPayLocation_Insert", userPayLocations, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertUsertDepartment(IEnumerable<UserDepartment> userDepartments)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_UserDepartment_Insert", userDepartments, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
