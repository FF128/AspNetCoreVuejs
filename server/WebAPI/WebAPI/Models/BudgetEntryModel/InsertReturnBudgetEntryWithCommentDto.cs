using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;

namespace WebAPI.Models.BudgetEntryModel
{
    public class InsertReturnBudgetEntryWithCommentDto
    {
        public string TransactionNo { get; set; }
        public BudgetEntryDto BudgetEntryDto { get; set; }
        public string Comment { get; set; }
        public IEnumerable<BudgetEntryDetailsDto> BudgetEntryDetails { get; set; }

        public List<IFormFile> Attachments { get; set; }
    }
}
