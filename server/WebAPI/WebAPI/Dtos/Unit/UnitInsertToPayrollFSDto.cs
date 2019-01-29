using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos.Unit
{
    public class UnitInsertToPayrollFSDto : BaseModel
    {
        public string UnitCode { get; set; }
        public string UnitDesc { get; set; } = "";
        public string Head { get; set; } = "";
        public string HeadCode { get; set; } = "";
        public string AccountCode { get; set; } = "";
        public string DBName { get; set; }
    }
}
