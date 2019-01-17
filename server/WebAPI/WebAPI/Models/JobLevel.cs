using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Job Level")]
    public class JobLevel : BaseModelCodeDescription
    {
        public int Id { get; set; }
        public string GradeCode { get; set; }
        public string StepCode { get; set; }
        public decimal MinimumSalary { get; set; }
        public decimal MaximumSalary { get; set; }
    }
}
