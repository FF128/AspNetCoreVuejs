using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.BudgetEntryModel;

namespace WebAPI.RepositoryInterfaces
{
    public interface IReturnedBudgetEntryRepository
    {
        Task<IEnumerable<ViewBudgetEntryWithStatusReturnedDto>> GetBudgetEntryWithStatusReturned();
        Task<IEnumerable<BudgetEntryMaintReturnComment>> GetBudgetEntryMaintReturnCommentsByTransNo(string transNo);
    }
}
