using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class UserDepartment
    {
        public long ID { get; set; }
        public string DepartmentCode { get; set; }
        public string EmpCode { get; set; }
        public string CompanyCode { get; set; }
    }
}
