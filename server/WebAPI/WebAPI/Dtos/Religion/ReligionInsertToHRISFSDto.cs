using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos.Religion
{
    public class ReligionInsertToHRISFSDto : BaseModel
    {
        public string RelCode { get; set; }
        public string RelDesc { get; set; }
        public string DBName { get; set; }
    }
}
