using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("Rank")]
    public class Rank : BaseModel
    {
        public int Id { get; set; }
        public string RankCode { get; set; }
        public string RankDesc { get; set; }
        public string CompanyCode { get; set; }
    }
}