using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class BaseModelCodeDescription : BaseModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        
        //public string CompanyCode { get; set; }
    }
}
