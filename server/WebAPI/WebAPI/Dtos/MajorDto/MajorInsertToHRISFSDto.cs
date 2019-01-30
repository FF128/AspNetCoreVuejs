using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos.MajorDto
{
    public class MajorInsertToHRISFSDto : BaseModel
    {
        public string MajorCode { get; set; }
        public string MajorDesc { get; set; }
        public string CompanyCode { get; set; }
        public string DBName { get; set; }
    }
}
