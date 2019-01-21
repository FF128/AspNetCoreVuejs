using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Screening Ratings")]
    public class Ratings : BaseModel
    {
        public int Id { get; set; }
        public string RatingsCode { get; set; }
        public string RatingsDesc { get; set; }
        public string CompanyCode { get; set; }
    }
}
