using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Grade")]
    public class Grade : BaseModelCodeDescription
    {
        public int Id { get; set; }
    }
}
