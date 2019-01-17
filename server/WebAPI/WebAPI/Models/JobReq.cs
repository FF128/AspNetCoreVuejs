using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("Job Requirements")]
    public class JobReq : BaseModel
    {
        public int Id { get; set; }
        public string JobReqCode { get; set; }
        public string JobReqDesc { get; set; }
        public string CompanyCode { get; set; }
    }
}