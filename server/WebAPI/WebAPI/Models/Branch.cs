using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Branch : BaseModel
    {
        public int Id { get; set; }
        public string BranchCode { get; set; }
        public string BranchDesc { get; set; }
        public string HeadCode { get; set; }
        public string AcctCode { get; set; }
    }
}