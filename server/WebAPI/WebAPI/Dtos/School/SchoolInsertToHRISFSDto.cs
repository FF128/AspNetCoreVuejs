using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos.School
{
    public class SchoolInsertToHRISFSDto  : BaseModel
    {
        public int Id { get; set; }
        public string SchoolCode { get; set; }
        public string SchoolDesc { get; set; } = "";
        public string SchoolAddress { get; set; } = "";
        public string CompanyCode { get; set; }
        public string DBName { get; set; }
    }
}
