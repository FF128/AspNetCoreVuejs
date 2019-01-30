using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos.GradeDto
{
    public class GradeInsertToFileSetupDto : BaseModel
    {
        public string GrdCode { get; set; }
        public string GrdDesc { get; set; }
        public string CompanyCode { get; set; }
        public string DBName { get; set; }
    }
}
