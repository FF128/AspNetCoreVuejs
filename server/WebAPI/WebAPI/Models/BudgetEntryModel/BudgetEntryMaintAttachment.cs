using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.BudgetEntryModel
{
    [Description("Budget Entry Maint Attachment")]
    public class BudgetEntryMaintAttachment
    {
        public long ID { get; set; }
        public string TransactionNo { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public string FolderName { get; set; }
    }
}
