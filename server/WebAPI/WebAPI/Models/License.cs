using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class License : BaseModel
    {
        public int Id { get; set; }
        public string LicenseCode { get; set; }
        public string LicenseDesc { get; set; }
    }
}