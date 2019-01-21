using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Overall Ratings")]
    public class OverallRatings : BaseModel
    {
        public int Id { get; set; }
        public string OverallRatingsCode { get; set; }
        public string OverallRatingsDesc { get; set; }
        public string CompanyCode { get; set; }
    }
}
