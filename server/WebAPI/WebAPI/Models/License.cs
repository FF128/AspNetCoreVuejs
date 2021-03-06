﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("License")]
    public class License : BaseModel
    {
        public int Id { get; set; }
        public string LicenseCode { get; set; }
        public string LicenseDesc { get; set; }
        public string CompanyCode { get; set; }
    }
}