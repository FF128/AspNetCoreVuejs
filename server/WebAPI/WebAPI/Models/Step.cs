using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Step")]
    public class Step: BaseModelCodeDescription
    {
        public int Id { get; set; }
    }
}
