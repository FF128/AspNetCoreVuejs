﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class School : BaseModel
    {
        public int Id { get; set; }
        public string SchoolCode { get; set; }
        public string SchoolDesc { get; set; }
        public string SchoolAddress { get; set; }
    }
}