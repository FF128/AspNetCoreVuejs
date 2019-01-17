using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Affiliations")]
    public class Affiliations : BaseModelCodeDescription
    {
        public int Id { get; set; }
        public string CompanyCode { get; set; }
    }
}
