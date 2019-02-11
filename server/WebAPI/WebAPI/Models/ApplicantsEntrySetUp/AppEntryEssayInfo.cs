using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.ApplicantsEntrySetUp
{
    [Description("Applicants Entry: Essay Questions Definition")]
    public class AppEntryEssayInfo : BaseModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public bool Active { get; set; }
        public string CompanyCode { get; set; }
    }
}
