using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("Unit")]
    public class Unit : BaseModel
    {
        public int Id { get; set; }
        public string UnitCode { get; set; }
        public string UnitDesc { get; set; } = "";
        public string Head { get; set; } = "";
        public string HeadCode { get; set; } = "";
        public string AcctCode { get; set; } = "";
        public string CompanyCode { get; set; }

    }
}