using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos
{
    public class CitizenshipInsertToFileSetUpDto
    {
        public string CitiCode { get; set; }
        public string CitiDesc { get; set; }
        public string DBName { get; set; }
        public string CreatedBy { get; set; }
    }
}
