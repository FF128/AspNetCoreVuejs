using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS_WebApiMvc.Models
{
    public class School : BaseModelCodeDescription
    {
        public int Id { get; set; }
        public string SchoolAddrs { get; set; }
    }
}