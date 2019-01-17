using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("Region")]
    public class Region : BaseModel
    {
        public int Id { get; set; }
        public string RegionCode { get; set; }
        public string RegionDesc { get; set; }
        public decimal MinimumWage { get; set; }
    }
}