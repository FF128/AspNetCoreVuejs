using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    [Description("Pay House")]
    public class PayHouse : BaseModel
    {
        public int Id { get; set; }
        public string PayHouseCode { get; set; }
        public string PayHouseDesc { get; set; }
        public string HeadCode { get; set; }
        public string HeadName { get; set; }
    }
}