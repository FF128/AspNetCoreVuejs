using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos.CitizenshipDto
{
    public class CitizenshipUpdateToFileSetUpDto : BaseModel
    {
        public string CitiCode { get; set; }
        public string CitiDesc { get; set; }
        public string DBName { get; set; }
    }
}
