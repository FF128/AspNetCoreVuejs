using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.PRModel
{
    public class PRFExtend
    {
        public long ID { get; set; }
        public string PRFNo { get; set; }
        public DateTime OldDateFrom { get; set; }
        public DateTime OldDateTo { get; set; }
        public DateTime OldDateRequired { get; set; }
        public DateTime NewDateFrom { get; set; }
        public DateTime NewDateTo { get; set; }
        public DateTime NewDateRequired { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; } = DateTime.Now;
        public string CompanyCode { get; set; }
    }
}
