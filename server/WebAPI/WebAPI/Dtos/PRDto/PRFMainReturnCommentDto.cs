using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.PRDto
{
    public class PRFMainReturnCommentDto
    {
        public long ID { get; set; }
        public string PRFNo { get; set; }
        public string Comment { get; set; }
        public string CommentedBy { get; set; }
        public DateTime CommentedDate { get; set; } = DateTime.Now;
    }
}
