using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class DutiesAndResponsibilities : BaseModel
    {
        public int Id { get;set; }
        public string DutiesResponsibilitiesCode { get; set; }
        public string DutiesResponsibilitiesDesc { get; set; }
    }
}