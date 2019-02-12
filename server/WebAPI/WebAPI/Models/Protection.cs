using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Protection
    {
        public string SKey { get; set; }
        public string Salt { get; set; }
        public int KeySize { get; set; }
    }
}
