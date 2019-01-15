using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class PayHouse : BaseModel
    {
        public int Id { get; set; }
        public string PayHouseCode { get; set; }
        public string PayHouseDesc { get; set; }
        public string HeadCode { get; set; }
        public string HeadName { get; set; }
    }
}