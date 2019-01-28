using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("Skills")]
    public class Skills : BaseModel
    {
        public int ID { get; set; }
        public string SkillsCode { get; set; }

        public string SkillsDesc { get; set; }
        public string CompanyCode { get; set; }
    }
}