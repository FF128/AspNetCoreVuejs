using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.PRModel;

namespace WebAPI.Dtos.PRDto
{
    public class InsertReturnPRFDto
    {
        public PRFHeaderMaint Header { get; set; }
        public IEnumerable<PRFDetailsMaint> Details { get; set; }
        public string Comment { get; set; }
    }
}
