using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Personnel Request Type")]
    public class PersonnelRequestType : BaseModel
    {
        public int Id { get; set; }
        public string PersonnelReqTypeCode { get; set; }
        public string PersonnelReqTypeDesc { get; set; }
        public string CompanyCode { get; set; }
    }
}
