using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class PersonnelRequestType : BaseModelCodeDescription
    {
        public int Id { get; set; }
        public string CompanyCode { get; set; }
    }
}
