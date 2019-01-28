using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("Government Exams")]
    public class GovExams : BaseModel
    {
        public int Id { get; set; }
        public string ExamCode { get; set; }
        public string ExamDesc { get; set; }
        public string CompanyCode { get; set; }
    }
}