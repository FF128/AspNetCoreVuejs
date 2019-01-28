using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("Duties and Responsibilities")]
    public class DutiesAndResponsibilities : BaseModel
    {
        public int Id { get;set; }
        public string DutiesResponsibilitiesCode { get; set; }
        public string DutiesResponsibilitiesDesc { get; set; }
        public string CompanyCode { get; set; }
    }
}