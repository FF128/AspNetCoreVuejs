using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class AuditTrail
    {
        public int Id { get; set; }
        public string EmpCode { get; set; }
        public string Trans { get; set; }
        public DateTime DateLog { get; set; } = DateTime.Now;
        public string Message { get; set; }
        public string ClientNetAddress { get; set; }
        public string HostName { get; set; }
    }
}
