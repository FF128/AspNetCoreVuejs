﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class DocSubmitted : BaseModel
    {
        public int ID { get; set; }
        public string DocCode { get; set; }
        public string DocDesc { get; set; }
    }
}