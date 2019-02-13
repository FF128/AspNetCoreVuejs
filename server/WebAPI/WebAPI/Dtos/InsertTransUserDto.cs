using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.TransUserModel;

namespace WebAPI.Dtos
{
    public class InsertTransUserDto
    {
        public TransUser TransUser { get; set; }
        public IEnumerable<TransUserPaylocation> TransUserPaylocations { get; set; }
        public IEnumerable<TransUserDepartment> TransUserDepartments { get; set; }
    }
}
