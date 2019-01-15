using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class CourseDegree : BaseModel
    {
        public int Id { get; set; }
        public string CourseDegreeCode { get; set; }
        public string CourseDegreeDesc { get; set; }
    }
}