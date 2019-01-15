using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Division : BaseModel
    {
        public int Id { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionDesc { get; set; }
        public string Head { get; set; }
        public string HeadCode { get; set; }
        public string AcctCode { get; set; }
    }
}