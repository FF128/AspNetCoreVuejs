using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.BudgetEntryModel
{
    [Description("Budget Entry Maint Header")]
    public class BudgetEntryMainHeader : BaseModel
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string TransactionNo { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public DateTime DurationFrom { get; set; }
        public DateTime DurationTo { get; set; }
        public string Approver { get; set; }
        public string CC { get; set; }
        public string Status { get; set; }
        public string EmpStatus { get; set; }
        public string Reason { get; set; }
        public string Remarks { get; set; }
        public DateTime DateRequired { get; set; }
        public string CompanyCode { get; set; }
    }
}
