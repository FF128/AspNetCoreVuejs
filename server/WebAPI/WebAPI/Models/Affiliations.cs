using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Affiliations/Membership to Organization")]
    public class Affiliations : BaseModel
    {
        public int Id { get; set; }
        public string AffiliationsCode { get; set; }
        public string AffiliationsDesc { get; set; }
        public string CompanyCode { get; set; }
    }
}
