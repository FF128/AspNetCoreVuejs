﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class DesignationFile : BaseModelCodeDescription
    {
        public int Id { get; set; }
        public string JobLevelCode { get; set; }
    }
}