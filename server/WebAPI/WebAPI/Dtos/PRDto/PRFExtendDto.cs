using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.PRDto
{
    public class PRFExtendDto
    {
        public string PRFNo { get; set; }
        public DateTime OldDateFrom { get; set; }
        public DateTime OldDateTo { get; set; }
        public DateTime OldDateRequired { get; set; }
        public DateTime NewDateFrom { get; set; }
        public DateTime NewDateTo { get; set; }
        public DateTime NewDateRequired { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
    }
}
