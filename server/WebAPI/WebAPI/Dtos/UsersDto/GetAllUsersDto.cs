using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.UsersDto
{
    public class GetAllUsersDto
    {
        public long ID { get; set; }
        public string EmpCode { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
    }
}
