using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos
{
    public class GetScreenDetailSubDto
    {
        public int Id { get; set; }
        public int ScreenDetailId { get; set; }
        public int ScreenOrder { get; set; }
        public string ScreenTypeCode { get; set; }
        public string ScreenTypeDesc { get; set; }
        public string CompanyCode { get; set; }
    }
}
