using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Unit : BaseModel
    {
        public int Id { get; set; }
        public string UnitCode { get; set; }
        public string UnitDesc { get; set; }
        public string Head { get; set; }
        public string HeadCode { get; set; }
        public string AcctCode { get; set; }

    }
}