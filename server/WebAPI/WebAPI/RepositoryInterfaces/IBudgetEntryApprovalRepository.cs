using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.BudgetEntry;
using WebAPI.Models.BudgetEntryModel;

namespace WebAPI.RepositoryInterfaces
{
    public interface IBudgetEntryApprovalRepository
    {
        Task<IEnumerable<ViewBudgetEntryWithStatusWaitingDto>> GetBudgetEntriesWithStatusWaiting();
        Task<BudgetEntryMaintDetails> GetByTransNo(string transNo, string dbName);
        Task<IEnumerable<BudgetEntryMaintDetails>> GetAllByTransNo(string transNo, string dbName);

        Task UpdateBudgetEntryDetailsStatusApproved(string transNo, string status);
        Task UpdateBudgetEntryDetailsStatusDeclined(string transNo, string status);
        Task UpdateBudgetEntryDetailsStatusReturned(BudgetEntryMaintReturnComment comment, string status);
        Task UpdateRemarks(UpdateRemarksBudgetEntryDetailDto dto);



    }
}
