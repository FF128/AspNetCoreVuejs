using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.PRDto
{
    public class GetExtendPRFDetails
    {
        public string PRFNo { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string HeaderStatus { get; set; }
        public string ExtendStatus { get; set; }
        public long TotalRecords { get; set; }
    }
}
