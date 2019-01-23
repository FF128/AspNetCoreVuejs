using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Job Group")]
    public class JobGroup: BaseModel
    {
        public int Id { get; set; }
        public string JobGroupCode { get; set; }
        public string JobGroupDesc { get; set; }
        public string CompanyCode { get; set; }
    }
}
