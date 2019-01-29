using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos.Location
{
    public class LocationInsertFileSetupDto : BaseModel
    {
        public string LocationCode { get; set; }
        public string LocationDesc { get; set; }
        public string Head { get; set; }
        public string HeadCode { get; set; }
        public string AcctCode { get; set; }
        public string DBName { get; set; }
    }
}
