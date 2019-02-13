using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.TransUserModel
{
    public class TransUser : BaseModel
    {
        public long ID { get; set; }
        public bool IsActive { get; set; }
        public string EmpCode { get; set; }
        public string Trans { get; set; }
        public int Order { get; set; }
        public string CompanyCode { get; set; }
    }
}
