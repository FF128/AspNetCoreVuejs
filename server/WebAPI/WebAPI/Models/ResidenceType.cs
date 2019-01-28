using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("Residence Type")]
    public class ResidenceType : BaseModel
    {
        public int Id { get; set; }
        public string ResidenceTypeCode { get; set; }
        public string ResidenceTypeDesc { get; set; }
        public string CompanyCode { get; set; }
    }
}