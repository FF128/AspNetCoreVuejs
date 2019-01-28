using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("Course Degree")]
    public class CourseDegree : BaseModel
    {
        public int Id { get; set; }
        public string CourseDegreeCode { get; set; }
        public string CourseDegreeDesc { get; set; }
        public string CompanyCode { get; set; }
    }
}