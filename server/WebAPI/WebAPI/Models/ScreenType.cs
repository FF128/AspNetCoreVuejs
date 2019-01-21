using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Type of Screening")]
    public class ScreenType : BaseModel
    {
        public int Id { get; set; }
        public string ScreenTypeCode { get; set; }
        public string ScreenTypeDesc { get; set; }
        public string CompanyCode { get; set; }
    }
}
