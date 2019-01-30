using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos.Division
{
    public class DivInsertToPayrollFSDto : BaseModel
    {
        public string DivCode { get; set; }
        public string DivDesc { get; set; } = "";
        public string DivHead { get; set; } = "";
        public string AccountCode { get; set; } = "";
        public string DBName { get; set; }

    }
}
