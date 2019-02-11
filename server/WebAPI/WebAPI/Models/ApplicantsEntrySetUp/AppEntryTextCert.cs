using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.ApplicantsEntrySetUp
{
    [Description("Applicants Entry: Text Certification")]
    public class AppEntryTextCert : BaseModel
    {
        public int Id { get; set; }
        public string TextCertification { get; set; }
        public string CompanyCode { get; set; }
    }
}
