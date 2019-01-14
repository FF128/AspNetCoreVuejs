using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS_WebApiMvc.Models
{
    public class Section : BaseModelCodeDescription
    {
        public int Id { get; set; }
        public string SecHead { get; set; }
        public string SecHeadCode { get; set; }
    }
}