using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Multi Company Database Setup")]
    public class MultiCompanyDatabase : BaseModel
    {
        public int Id { get; set; }
        public string DatabaseCode { get; set; }
        public string DatabaseDesc { get; set; }
        public bool IsActive { get; set; }
        public string CompanyCode { get; set; }
    }
}
