using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS_WebApiMvc.Models
{
    public class PayHouse : BaseModelCodeDescription
    {
        public int Id { get; set; }
        public string HeadCode { get; set; }
        public string HeadName { get; set; }
    }
}