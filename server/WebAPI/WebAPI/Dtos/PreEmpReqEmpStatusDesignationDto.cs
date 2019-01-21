using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos
{
    public class PreEmpReqEmpStatusDesignationDto
    {
        public int Id { get; set; }
        public string PreEmpReqCode { get; set; }
        public string PreEmpReqDesc { get; set; }
        public string EmploymentStatusCode { get; set; }
        public string EmploymentStatus { get; set; }
        public string Designation { get; set; }
        public string DesignationCode { get; set; }
        public string CompanyCode { get; set; }
    }
}
