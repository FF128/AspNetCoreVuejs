using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.BudgetEntry;

namespace WebAPI.Models.BudgetEntryModel
{
    public class GetBudgetEntryApprovalDetailsDto
    {
        public BudgetEntryMainHeader BudgetEntryMainHeader { get; set; }
        public IEnumerable<GetBudgetEntryMaintDetails> BudgetEntryMaintDetails { get; set; }
        public IEnumerable<BudgetEntryMaintAttachment> BudgetEntryMaintAttachments { get; set; }
    }
}
