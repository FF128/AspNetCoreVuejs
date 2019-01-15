using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class ResidenceType : BaseModel
    {
        public int Id { get; set; }
        public string ResidenceTypeCode { get; set; }
        public string ResidenceTypeDesc { get; set; }
    }
}