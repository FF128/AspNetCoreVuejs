using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.PRModel
{
    public class TransApprovingPRF : BaseModel
    {
        public long ID { get; set; }
        public string PRFNo { get; set; }
        public long PRFDetailsID { get; set; }
        public string Approver { get; set; }
        public string Status { get; set; }
        public bool IsDone { get; set; }
        public int Order { get; set; }
        public DateTime DateApproved { get; set; }
        public string CompanyCode { get; set; }
    }
}
