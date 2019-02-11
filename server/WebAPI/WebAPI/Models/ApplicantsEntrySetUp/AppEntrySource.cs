using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.ApplicantsEntrySetUp
{
    [Description("Applicants Entry: Source of Information")]
    public class AppEntrySource : BaseModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Desc { get; set; }
        public bool Active { get; set; }
        public string CompanyCode { get; set; }
    }
}
