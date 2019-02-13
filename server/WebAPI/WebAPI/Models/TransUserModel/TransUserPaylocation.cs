using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.TransUserModel
{
    public class TransUserPaylocation
    {
        public long ID { get; set; }
        public long TransID { get; set; }
        public long LocId { get; set; }
        public string CompanyCode { get; set; }
    }
}
