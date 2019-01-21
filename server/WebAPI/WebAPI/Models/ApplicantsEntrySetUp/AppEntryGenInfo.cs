using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.ApplicantsEntrySetUp
{
    [Description("Applicants Entry: General Information")]
    public class AppEntryGenInfo : BaseModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public bool ShowYes { get; set; }
        public bool ShowNo { get; set; }
        public bool ShowNone { get; set; }
        public bool ShowWhy { get; set; }
        public string ShowWhyText { get; set; }
        public bool Active { get; set; }
    }
}
