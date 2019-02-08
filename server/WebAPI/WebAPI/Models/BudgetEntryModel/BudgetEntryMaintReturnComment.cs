using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.BudgetEntryModel
{
    [Description("Budget Entry Return Comment")]
    public class BudgetEntryMaintReturnComment
    {
        public long ID { get; set; }
        public string TransactionNo { get; set; }
        public string Comment { get; set; }
        public string CommentedBy { get; set; }
        public DateTime CommentedDate { get; set; } = DateTime.Now;
    }
}
