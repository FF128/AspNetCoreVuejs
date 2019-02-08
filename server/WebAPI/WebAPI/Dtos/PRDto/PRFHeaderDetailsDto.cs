using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.PRDto
{
    public class PRFHeaderDetailsDto
    {
        public PRFHeaderMaintDto Header { get; set; }
        public IEnumerable<PRFDetailsMaintDto> Details { get; set; }
    }
}
