using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.PRModel;

namespace WebAPI.Dtos.PRDto
{
    public class PRFHeaderDetailsDto
    {
        public PRFHeaderMaintDto Header { get; set; }
        public IEnumerable<PRFDetailsMaintDto> Details { get; set; }
        public IEnumerable<TransApprovingPRF> TransApproving { get; set; }
        public List<IFormFile> Attachments { get; set; }
        public bool IsBudgeted { get; set; }
    }
}
