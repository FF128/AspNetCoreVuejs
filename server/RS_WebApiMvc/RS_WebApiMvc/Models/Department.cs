using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS_WebApiMvc.Models
{
    public class Department : BaseModelCodeDescription
    {
        public int Id { get; set; }
        public string DepHeadCode { get; set; }
    }
}