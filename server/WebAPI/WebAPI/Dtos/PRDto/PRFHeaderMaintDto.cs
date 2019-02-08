using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos.PRDto
{
    public class PRFHeaderMaintDto : BaseModel
    {
        public string PRFNo { get; set; }
        public string CC { get; set; }
        public string EmpStatus { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateRequired { get; set; }
        public DateTime DurationFrom { get; set; }
        public DateTime DurationTo { get; set; }
        public string RequestedBy { get; set; }
        public string Reason { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string Approver { get; set; }
        public string Description { get; set; }
        public string CompanyCode { get; set; }
    }
}
