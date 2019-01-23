using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Email Format Setup")]
    public class EmailFormat : BaseModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string TransType { get; set; }
        public bool IsActive { get; set; }
        public string CompanyCode { get; set; }
    }
}
