using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos
{
    public class ScreenDetailsDto : BaseModel
    {
        public int Id { get; set; }
        public string ScreenDetailsCode { get; set; }
        public string ScreenDetailsDesc { get; set; }
        public string CompanyCode { get; set; }

        //public IEnumerable<ScreenDetailsSub> ScreenDetailsSubs { get; set; }
    }
}
