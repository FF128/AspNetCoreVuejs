using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS_WebApiMvc.Models
{
    public class BaseModelCodeDescription : BaseModel
    {
       
        public string Code { get; set; }
        public string Description { get; set; }
        
        //public string CompanyCode { get; set; }
    }
}
