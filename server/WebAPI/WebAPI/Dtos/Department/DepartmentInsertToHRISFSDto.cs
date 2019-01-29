using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos.Department
{
    public class DepartmentInsertToHRISFSDto : BaseModel
    {
        public string DepCode { get; set; }
        public string DepDesc { get; set; } = "";
        public string DepHeadCode { get; set; } = "";
        public string DepHead { get; set; } = "";
        public string DivCode { get; set; } = "";
        public string DBName { get; set; }
    }
}
