using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos
{
    public class DesignationDutiesReqDto : BaseModel
    {
        public int Id { get; set; }
        public string DesignationCode { get; set; }
        public IEnumerable<DesignationDutiesResponsibilities> DesignationDutiesResponsibilities { get; set; }
        public IEnumerable<DesignationDutiesJobReq> DesignationDutiesJobReqs { get; set; }
    }
}
