using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.PRModel
{
    public class ViewPREntryWithStatusDto
    {
        public long ID { get; set; }
        public string PRFNo { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
