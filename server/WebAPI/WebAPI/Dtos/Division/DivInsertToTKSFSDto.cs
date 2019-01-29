using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.Division
{
    public class DivInsertToTKSFSDto
    {
        public string DivCode { get; set; }
        public string DivDesc { get; set; }
        public string DivHead { get; set; }
        public string DivHeadCode { get; set; }
        public string DBName { get; set; }
    }
}
