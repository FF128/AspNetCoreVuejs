using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.BudgetEntry
{
    public class UpdateTransApprovingStatusDto
    {
        public string TransNo { get; set; }
        public string Status { get; set; }
        public bool IsDone { get; set; }
        public string CompanyCode { get; set; }
    }
}
