using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Section : BaseModel
    {
        public int Id { get; set; }
        public string SectionCode { get; set; }
        public string SectionDesc { get; set; }
        public string SecHead { get; set; }
        public string SecHeadCode { get; set; }
    }
}