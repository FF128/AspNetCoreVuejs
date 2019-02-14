using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.BudgetEntryModel
{
    public class TransApprovingBudget : BaseModel
    {
        public long ID { get; set; }
        public string TransactionNo { get; set; }
        public long BudgetDetailsID { get; set; }
        public string Approver { get; set; }
        public string Status { get; set; }
        public bool IsDone { get; set; }
        public int Order { get; set; }
        public DateTime DateApproved { get; set; } = DateTime.Now;
        public string CompanyCode { get; set; }
    }
}
