using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos
{
    public class GetDesignationDutiesReqDto
    {
        public string DesignationCode { get; set; }
        public string Description { get; set; }
        //public string DutiesRes { get; set; }
        //public string JobReq { get; set; }
        public List<string> DutiesRes { get; set; }
        public List<string> JobReq { get; set; }
        public IEnumerable<DutiesAndResponsibilities> DutiesAndResponsibilities { get; set; }
        public IEnumerable<JobReq> JobReqs { get; set; }
    }
}
