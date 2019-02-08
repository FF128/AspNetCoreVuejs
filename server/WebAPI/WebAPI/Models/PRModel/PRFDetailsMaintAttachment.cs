using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.PRModel
{
    public class PRFDetailsMaintAttachment
    {
        public long ID { get; set; }
        public string PRFNo { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public string FolderName { get; set; }
    }
}
