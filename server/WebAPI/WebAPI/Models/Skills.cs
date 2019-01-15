using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Skills : BaseModel
    {
        public int ID { get; set; }
        public string SkillsCode { get; set; }

        public string SkillsDesc { get; set; }
    }
}