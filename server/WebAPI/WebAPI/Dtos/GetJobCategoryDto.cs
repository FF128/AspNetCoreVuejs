using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos
{
    public class GetJobCategoryDto
    {
        public string JobCatCode { get; set; }
        public string JobCatDesc { get; set; }
        public string JobGroupCode { get; set; }
        public string JobGroupDesc { get; set; }
        public IEnumerable<DesignationFile> DesignationFiles { get; set; }
    }
}
