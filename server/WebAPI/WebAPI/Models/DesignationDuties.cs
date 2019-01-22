using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Designation Duties and Requirements")]
    public class DesignationDuties : BaseModel
    {
        public int Id { get; set; }
        public string DesignationCode { get; set; }

    }
    public class DesignationDutiesResponsibilities
    {
        public int Id { get; set; }
        public string DesignationCode { get; set; }
        public string DutiesResponsibilitiesCode { get; set; }
    }
    public class DesignationDutiesJobReq
    {
        public int Id { get; set; }
        public string DesignationCode { get; set; }
        public string JobReqCode { get; set; }
    }
}
