using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.BudgetEntry;
using WebAPI.Models;
using WebAPI.Models.BudgetEntryModel;

namespace WebAPI.RepositoryInterfaces
{
    public interface IBudgetEntryRepository
    {
        Task<int> Insert(BudgetEntryMainHeader budgetEntryMainHeader);
        Task InsertAllocated(IEnumerable<BudgetEntryMaintAllocated> budgetEntryMaintAllocated);
        Task<BudgetEntryMainHeader> GetByTransNo(string transNo);
        Task<IEnumerable<GetAvailableBudgetEntriesDto>> GetBudgetEntriesByStatus(string status, string dbName);
        Task<IEnumerable<GetAvailableBudgetEntriesDto>> GetBudgetEntriesByBudgetDetailsId(long[] id);

        Task<IEnumerable<BudgetEntryMainHeader>> GetAll();
        Task<IEnumerable<GetBudgetEntryMaintDetails>> GetBudgetEntryMaintDetails();
        Task InsertDetails(IEnumerable<BudgetEntryMaintDetails> budgetEntryMaintDetails);
        Task<IEnumerable<GetBudgetEntryMaintDetails>> GetBudgetEntryMaintDetailsByTransNo(string transNo, string dbName);
        
        Task InsertAttachments(IEnumerable<BudgetEntryMaintAttachment> budgetEntryMaintAttachments);
        Task<IEnumerable<BudgetEntryMaintAttachment>> GetBudgetEntryMaintAttachmentsByTransNo(string transNo);
        GetBudgetEntryApprovalDetailsDto GetBudgetEntryApprovalDetailsDtoByTransNo(string transNo);
        Task InsertComment(BudgetEntryMaintReturnComment comment);
        Task InsertTransApproving(IEnumerable<TransApprovingBudget> transApprovingBudget);
        Task Update(BudgetEntryMainHeader budgetEntryMainHeader);
        Task UpdateTransNo(int id, string transNo);
        Task UpdateStatus(string transNo, string status);
        Task DeleteByTransNo(string transNo);
    }
}
