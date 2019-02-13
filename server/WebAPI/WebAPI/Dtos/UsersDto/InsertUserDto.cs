using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos.UsersDto
{
    public class InsertUserDto
    {
        public UserDto User { get; set; }
        public IEnumerable<UserDepartment> UserDepartments { get; set; }
        public IEnumerable<UserPayLocation> UserPayLocations { get; set; }
    }
}
