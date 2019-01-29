using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos.Section
{
    public class SectionInsertToPayrollFSDto : BaseModel
    {
        public string SecCode { get; set; }
        public string DepCode { get; set; } = "";
        public string SecDesc { get; set; } = "";
        public string SecHead { get; set; } = "";
        public string AccountCode { get; set; } = "";
        public string DBName { get; set; }
    }
}
