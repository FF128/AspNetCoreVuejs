using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.BudgetEntry
{
    public class InsertReturnBudgetEntryDto
    {
        public string TransactionNo { get; set; }
        public string BudgetEntry { get; set; }
        public string BudgetEntryDetails { get; set; }
        public string Comment { get; set; }
        public List<IFormFile> files { get; set; }
    }
}
