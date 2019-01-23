using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Description("Job Category")]
    public class JobCategory : BaseModel
    {
        public int Id { get; set; }
        public string JobCatCode { get; set; }
        public string JobCatDesc { get; set; }
        public string JobGroupCode { get; set; }
        public string CompanyCode { get; set; }

    }
    public class JobCategoryDetail
    {
        public int Id { get; set; }
        public string JobCatCode { get; set; }
        public string DesignationFileCode { get; set; }
    }
}
