using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS_WebApiMvc.Models
{
    public class JobLevel : BaseModelCodeDescription
    {
        public int Id { get; set; }
        public string GradeCode { get; set; }
        public string StepCode { get; set; }
        public decimal MinimumSalary { get; set; }
        public decimal MaximumSalary { get; set; }
    }
}
