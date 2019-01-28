using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("Area")]
    public class Area : BaseModel
    {
        public int Id { get; set; }
        public string AreaCode { get; set; }
        public string AreaDesc { get; set; }
        public string HeadCode { get; set; }
        public string AcctCode { get; set; }
        public string CompanyCode { get; set; }
    }
}