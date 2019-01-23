using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Designation File")]
    public class DesignationFile : BaseModel
    {
        public int Id { get; set; }
        public string DesignationFileCode { get; set; }
        public string DesignationFileDesc { get; set; }
        public string JobLevelCode { get; set; }
    }
}
