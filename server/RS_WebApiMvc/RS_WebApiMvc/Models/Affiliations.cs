using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS_WebApiMvc.Models
{
    public class Affiliations : BaseModelCodeDescription
    {
        public int Id { get; set; }
        public string CompanyCode { get; set; }
    }
}
