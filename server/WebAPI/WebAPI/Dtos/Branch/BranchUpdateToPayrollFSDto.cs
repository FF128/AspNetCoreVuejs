using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos.Branch
{
    public class BranchUpdateToPayrollFSDto: BaseModel
    {
        public string BraCode { get; set; }
        public string BraDesc { get; set; } = "";
        public string BraMngr { get; set; } = "";
        public string DepCode { get; set; } = "";
        public string AccountCode { get; set; } = "";
        public string DBName { get; set; }
    }
}
