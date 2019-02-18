using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.PRModel;

namespace WebAPI.Dtos.PRDto
{
    public class PaginationPRFHeaderMaintDto : PRFHeaderMaint
    {
        public int TotalRecords { get; set; }
    }
}
