using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.PRModel
{
    public class PRFDetailsMaint : BaseModel
    {
        public long ID { get; set; }
        public string PRFNo { get; set; }
        public bool IsBudgeted { get; set; }
        public string BudgetTransNo { get; set; }
        public long BudgetDetailsID { get; set; }
        public string DesignationCode { get; set; }
        public string DepCode { get; set; }
        public string DivisionCode { get; set; }
        public string BraCode { get; set; }
        public string UnitCode { get; set; }
        public string SectionCode { get; set; }
        public string Status { get; set; }
        public long LocId { get; set; }
        public decimal AgreedSalary { get; set; }
        public string DatabaseCode { get; set; }
        public string LocCode { get; set; }
        public string GrpCode { get; set; }
        public string ApproverRemarks { get; set; }
        public string AreaCode { get; set; }
        public string ProjectCode { get; set; }
        public string RankCode { get; set; }
        public string PayHouseCode { get; set; }
        public string RegionCode { get; set; }
        public string EmployeeCategoryCode { get; set; }

    }
}
