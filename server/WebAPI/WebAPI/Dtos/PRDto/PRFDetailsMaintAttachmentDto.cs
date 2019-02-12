using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.PRDto
{
    public class PRFDetailsMaintAttachmentDto
    {
        public int ID { get; set; }
        public string PRFNo { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public string FolderName { get; set; }
    }
}
