using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos.CourseDto
{
    public class CourseInsertToHRISFSDto : BaseModel
    {
        public int Id { get; set; }
        public string CourseDegreeCode { get; set; }
        public string CourseDegreeDesc { get; set; }
        public string CompanyCode { get; set; }
        public string DBName { get; set; }
    }
}
