using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class BudgetEntryMaintAllocated : BaseModel
    {
        public long ID { get; set; }
        public bool IsBudgetted { get; set; }
        public string Status { get; set; }
        public string TransactionNo { get; set; }
        public long BudgetDetailsID { get; set; }
        public string PRFNo { get; set; }
        public string PRFDetailsID { get; set; }
        public string JobseekID { get; set; }
        public bool Exported { get; set; }
        public int PayLocID { get; set; }

    }
}
