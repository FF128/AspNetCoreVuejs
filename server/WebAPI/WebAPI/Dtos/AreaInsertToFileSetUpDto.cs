using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos
{
    public class AreaInsertToFileSetUpDto : BaseModel
    {
        public string AreaCode { get; set; }
        public string AreaDesc { get; set; }
        public string Head { get; set; } = "";
        public string HeadCode { get; set; } = "";
        public string AcctCode { get; set; } = "";
        public string DBName { get; set; }
    }
}
