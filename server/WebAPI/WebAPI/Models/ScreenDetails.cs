using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Screen Details")]
    public class ScreenDetails : BaseModel
    {
        public int Id { get; set; }
        public string ScreenDetailsCode { get; set; }
        public string ScreenDetailsDesc { get; set; }
        public string CompanyCode { get; set; }
    }
    [Description("Screen Details Sub")]
    public class ScreenDetailsSub : BaseModel
    {
        public int Id { get; set; }
        public int ScreenDetailId { get; set; }
        public int ScreenOrder { get; set; }
        public string ScreenTypeCode { get; set; }
        public string CompanyCode { get; set; }
    }
}
