using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("Section")]
    public class Section : BaseModel
    {
        public int Id { get; set; }
        public string SectionCode { get; set; }
        public string SectionDesc { get; set; } = "";
        public string SecHead { get; set; } = "";
        public string SecHeadCode { get; set; } = "";
        public string CompanyCode { get; set; }
    }
}