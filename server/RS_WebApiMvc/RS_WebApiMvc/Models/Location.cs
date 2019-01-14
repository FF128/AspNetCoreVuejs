﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS_WebApiMvc.Models
{
    public class Location : BaseModelCodeDescription
    {
        public int Id { get; set; }
        public string Head { get; set; }
        public string HeadCode { get; set; }
        public string AcctCode { get; set; }
    }
}