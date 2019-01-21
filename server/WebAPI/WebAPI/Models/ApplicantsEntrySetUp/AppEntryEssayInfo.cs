using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.ApplicantsEntrySetUp
{
    public class AppEntryEssayInfo : BaseModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public bool Active { get; set; }
    }
}
