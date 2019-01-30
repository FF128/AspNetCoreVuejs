using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.Branch
{
    public class BranchUpdateToTKSFSDto
    {
        public string BraCode { get; set; }
        public string BraDesc { get; set; } = "";
        public string BraMngr { get; set; } = "";
        public string BraMngrCode { get; set; } = "";
        public string DBName { get; set; }
    }
}
