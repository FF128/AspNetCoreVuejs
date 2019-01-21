using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Pre-Employment Requirements")]
    public class PreEmpReq : BaseModel
    {
        public int Id { get; set; }
        public string PreEmpReqCode { get; set; }
        public string PreEmpReqDesc { get; set; }
        public string EmploymentStatusCode { get; set; }
        public string DesignationCode { get; set; }
        public string CompanyCode { get; set; }
    }
}
