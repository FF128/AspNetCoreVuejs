using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos.Division
{
    public class DivInsertFileSetupDto : BaseModel
    {
        public string DivCode { get; set; }
        public string DivDesc { get; set; } = "";
        public string Head { get; set; } = "";
        public string HeadCode { get; set; } = "";
        public string AcctCode { get; set; } = "";
    }
}
