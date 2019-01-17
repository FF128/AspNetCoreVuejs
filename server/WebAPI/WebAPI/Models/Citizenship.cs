using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Citizenship")]
    public class Citizenship: BaseModelCodeDescription
    {
        public int Id { get; set; }
    }
}
