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
        public bool IsLocked { get; set; }
        public bool Active { get; set; }
        public bool AllowExpiDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CompanyCode { get; set; }
        public string EmailAddress { get; set; }
    }
}
