using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS_WebApiMvc.Models
{
    public class ProjectCode : BaseModelCodeDescription
    {
        public int Id { get; set; }
        public string ProjectHeadCode { get; set; }
        public string ProjectHeadName { get; set; }
    }
}