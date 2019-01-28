using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("Field of Interest")]
    public class FieldOfInterest : BaseModel
    {
        public int Id { get; set; }
        public string InterestCode { get; set; }
        public string InterestDesc { get; set; }
        public string CompanyCode { get; set; }
    }
}