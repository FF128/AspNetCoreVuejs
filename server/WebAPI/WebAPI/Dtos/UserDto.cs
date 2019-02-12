using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public bool AllowExpiDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string EmpCode { get; set; }
        public string CompanyCode { get; set; }
    }
}
