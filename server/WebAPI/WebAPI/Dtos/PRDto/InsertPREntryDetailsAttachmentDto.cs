using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.PRDto
{
    public class InsertPREntryDetailsAttachmentDto
    {
        public string Header { get; set; }
        public string Details { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
