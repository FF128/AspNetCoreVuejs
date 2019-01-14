using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RS_WebApiMvc.Models
{
    public class CompanyInformation : BaseModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public int ZipCode { get; set; }
        public int TelNum { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string TIN { get; set; }
        public string SSS { get; set; }
        public string Philhealth { get; set; }
        public string Pagibig { get; set; }
        public int BIRBranchCode { get; set; }

        public byte[] LogoForReports { get; set; } = null;
        public byte[] LogoForSite { get; set; } = null;
        public byte[] ContentForSite { get; set; } = null;
        public HttpPostedFile LogoForReportsFile { get; set; }
        public HttpPostedFile LogoForSiteFile { get; set; }
        public HttpPostedFile ContentForSiteFile { get; set; }
    }
    //public class CompanyInformationMapper : ClassMapper<CompanyInformation>
    //{
    //    public CompanyInformationMapper()
    //    {
    //        Table("Person");
    //        Map(m => m.Phones).Ignore();
    //        AutoMap();
    //    }
    //}
}
