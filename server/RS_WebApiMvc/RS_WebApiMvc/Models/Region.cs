using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS_WebApiMvc.Models
{
    public class Region : BaseModelCodeDescription
    {
        public int Id { get; set; }
        public decimal MinimumWage { get; set; }
    }
}