using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Region : BaseModel
    {
        public int Id { get; set; }
        public string RegionCode { get; set; }
        public string RegionDesc { get; set; }
        public decimal MinimumWage { get; set; }
    }
}