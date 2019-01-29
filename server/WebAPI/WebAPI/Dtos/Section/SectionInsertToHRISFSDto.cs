using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.Section
{
    public class SectionInsertToHRISFSDto
    {
        public string SecCode { get; set; }
        public string DepCode { get; set; } = "";
        public string SecDesc { get; set; } = "";
        public string SecHead { get; set; } = "";
        public string SecHeadCode { get; set; } = "";
        public string DBName { get; set; }
    }
}
