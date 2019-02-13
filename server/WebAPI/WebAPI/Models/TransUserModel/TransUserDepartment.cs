using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.TransUserModel
{
    public class TransUserDepartment
    {
        public long ID { get; set; }
        public long TransID { get; set; }
        public string DepartmentCode { get; set; }
        public string CompanyCode { get; set; }
    }
}
