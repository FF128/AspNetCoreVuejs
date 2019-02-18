using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Dtos.PRDto
{
    public class GetPREntryMaintDetails : BaseModel
    {
        public long ID { get; set; }
        public string PRFNo { get; set; }
        public string DepCode { get; set; }
        public string Department { get; set; }
        public string DivisionCode { get; set; }
        public string Division { get; set; }
        public string BraCode { get; set; }
        public string Branch { get; set; }
        public string UnitCode { get; set; }
        public string Unit { get; set; }
        public string SectionCode { get; set; }
        public string Section { get; set; }
        public string DesignationCode { get; set; }
        public string Designation { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string Approver { get; set; }
        public long LocId { get; set; }
        public string LocName { get; set; }
        public decimal AgreedSalary { get; set; }
        public string DatabaseCode { get; set; }
        public string Database { get; set; }
        public string LocCode { get; set; }
        public string Location { get; set; }
        public string GrpCode { get; set; }
        public string Group { get; set; }
        public string ApproverRemarks { get; set; }
        public string AreaCode { get; set; }
        public string Area { get; set; }
        public string ProjectCode { get; set; }
        public string Project { get; set; }
        public string RankCode { get; set; }
        public string Rank { get; set; }
        public string PayHouseCode { get; set; }
        public string PayHouse { get; set; }
        public string RegionCode { get; set; }
        public string Region { get; set; }
        public string EmployeeCategoryCode { get; set; }
    }
}
