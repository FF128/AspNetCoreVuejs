using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.PRDto
{
    public class PRFMainReturnCommentDto
    {
        public string Comment { get; set; }
        public string CommentedBy { get; set; }
        public DateTime CommentedDate { get; set; }
    }
}
