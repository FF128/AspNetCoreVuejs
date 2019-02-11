using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.PRModel;

namespace WebAPI.Dtos.PRDto
{
    public class GetPREntryApprovalDetailsDto
    {
        public PRFHeaderMaintDto Header { get; set; }
        public IEnumerable<GetPREntryDetails> Details { get; set; }
        public IEnumerable<PRFDetailsMaintAttachmentDto> Attachments { get; set; }
    }
}
