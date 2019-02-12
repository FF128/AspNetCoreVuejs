using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Dtos.BudgetEntry;
using WebAPI.Helpers;
using WebAPI.Models.BudgetEntryModel;

namespace WebAPI.ServiceInterfaces
{
    public interface IBudgetEntryService
    {
        Task<CustomMessage> Insert(BudgetEntryAndEntryDetailsDto dto);
        Task<CustomMessage> InsertReturnBudget(InsertReturnBudgetEntryWithCommentDto dto);
        Task<GetBudgetEntryApprovalDetailsDto> GetBudgetEntryApprovalDetails(string transNo);
        Task<IEnumerable<GetBudgetEntryMaintDetails>> GetBudgetEntryMaintDetailsByTransNo(string transNo);

       Task<IEnumerable<GetAvailableBudgetEntriesDto>> GetBudgetEntriesByStatus(string status);

        Task<CustomMessage> AcceptEntry(string transNo);
        Task<CustomMessage> DeclineEntry(string transNo);
        Task<CustomMessage> ReturnEntry(BudgetEntryMaintReturnComment comment);
    }
}
