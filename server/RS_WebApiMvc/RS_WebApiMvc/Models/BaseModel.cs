using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS_WebApiMvc.Models
{
    public class BaseModel
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string EditedBy { get; set; }
        public DateTime EditedDate { get; set; } = DateTime.Now;
    }
}
