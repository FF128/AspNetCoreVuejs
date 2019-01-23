using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos
{
    public class JobCategoryDto : BaseModel
    {
        public int Id { get; set; }
        public string JobCatCode { get; set; }
        public string JobCatDesc { get; set; }
        public string JobGroupCode { get; set; }
        public string CompanyCode { get; set; }

        public IEnumerable<JobCategoryDetail> JobCategoryDetails { get; set; }
    }
}
