using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Major : BaseModel
    {
        public int Id { get; set; }
        public string MajorCode { get; set; }
        public string MajorDesc { get; set; }
    }
}