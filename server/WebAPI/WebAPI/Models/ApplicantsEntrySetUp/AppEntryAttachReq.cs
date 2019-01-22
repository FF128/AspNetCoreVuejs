using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.ApplicantsEntrySetUp
{
    [Description("Applicants Entry: Attachments Required")]
    public class AppEntryAttachReq : BaseModel
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public bool Active { get; set; }
    }
}
