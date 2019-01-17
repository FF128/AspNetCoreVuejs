using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos
{
    public class CompanyInfoDto : BaseModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public int ZipCode { get; set; }
        public int TelNum { get; set; }
        public string Email { get; set; }
        public string TIN { get; set; }
        public string SSS { get; set; }
        public string Philhealth { get; set; }
        public string Pagibig { get; set; }
        public int BIRBranchCode { get; set; }
        public bool TKSFlag { get; set; }
        public bool PayrollFlag { get; set; }
        public bool HRISFlag { get; set; }
        public bool EmpOnlineFlag { get; set; }
        public string TKSDB { get; set; }
        public string PayrollDB { get; set; }
        public string HRISDB { get; set; }
        public string OnlineDB { get; set; }

        public byte[] LogoForReports { get; set; }
        public byte[] LogoForSite { get; set; }
        public byte[] ContentForSite { get; set; }
    }
}
