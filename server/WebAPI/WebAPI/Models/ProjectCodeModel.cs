using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class ProjectCodeModel : BaseModel
    {
        public int Id { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectDesc { get; set; }
        public string ProjectHeadCode { get; set; }
        public string ProjectHeadName { get; set; }
    }
}