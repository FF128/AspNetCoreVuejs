using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Area : BaseModel
    {
        public int Id { get; set; }
        public string AreaCode { get; set; }
        public string AreaDesc { get; set; }
        public string HeadCode { get; set; }
        public string AcctCode { get; set; }
    }
}