using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("Project Code")]
    public class ProjectCodeModel : BaseModel
    {
        public int Id { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectDesc { get; set; }
        public string ProjectHeadCode { get; set; }
        public string ProjectHeadName { get; set; }
        public string CompanyCode { get; set; }
    }
}