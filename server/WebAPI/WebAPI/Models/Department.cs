using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Department : BaseModel
    {
        public int Id { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentDesc { get; set; }
        public string DepHeadCode { get; set; }
    }
}