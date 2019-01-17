using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("Department")]
    public class Department : BaseModel
    {
        public int Id { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentDesc { get; set; }
        public string DepHeadCode { get; set; }
    }
}