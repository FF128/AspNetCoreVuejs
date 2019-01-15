using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Rank : BaseModel
    {
        public int Id { get; set; }
        public string RankCode { get; set; }
        public string RankDesc { get; set; }
    }
}