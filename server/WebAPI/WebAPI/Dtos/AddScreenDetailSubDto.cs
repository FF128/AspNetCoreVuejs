using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos
{
    public class AddScreenDetailSubDto
    {
        public int Id { get; set; }
        public IEnumerable<ScreenDetailsSub> ScreenDetailsSubs { get; set; }
    }
}
