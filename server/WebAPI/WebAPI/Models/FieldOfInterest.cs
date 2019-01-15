using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class FieldOfInterest : BaseModel
    {
        public int Id { get; set; }
        public string InterestCode { get; set; }
        public string InterestDesc { get; set; }
    }
}