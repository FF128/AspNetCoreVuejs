using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.DepartmentDto;

namespace WebAPI.Dtos.UsersDto
{
    public class GetUsersDepPayLocDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public bool AllowExpiDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string EmpCode { get; set; }
        public bool Active { get; set; }
        public bool IsLocked { get; set; }
        public IEnumerable<PayLocationDto> PayLocations { get; set; }
        public IEnumerable<GetDeparmentDto> Departments { get; set; }
    }
}
