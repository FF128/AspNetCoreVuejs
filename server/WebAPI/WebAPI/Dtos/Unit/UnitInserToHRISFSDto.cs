using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.Unit
{
    public class UnitInsertToHRISFSDto
    {
        public string UnitCode { get; set; }
        public string UnitDesc { get; set; } = "";
        public string Head { get; set; } = "";
        public string HeadCode { get; set; } = "";
        public string AccountCode { get; set; } = "";
        public string DBName { get; set; }

    }
}
