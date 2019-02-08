using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.BudgetEntryModel
{
    public class ViewBudgetEntryWithStatusReturnedDto
    {
        public long ID { get; set; }
        public string TransactionNo { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
