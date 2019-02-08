using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.BudgetEntryModel;

namespace WebAPI.Dtos
{
    public class BudgetEntryDto
    {
        public string Description { get; set; }
        public DateTime DateRequired { get; set; }
        public DateTime DurationFrom { get; set; }
        public DateTime DurationTo { get; set; }
        public string EmpStatus { get; set; }
        public string Reason { get; set; }
        public string Remarks { get; set; }
    }
    public class BudgetEntryDetailsDto
    {
        public string DatabaseCode { get; set; }
        public int LocId { get; set; }
        public string LocName { get; set; }
        public string DepCode { get; set; }
        public string LocationCode { get; set; }
        public string GroupCode { get; set; }
        public string DivisionCode { get; set; }
        public string AreaCode { get; set; }
        public string BraCode { get; set; }
        public string UnitCode { get; set; }
        public string SectionCode { get; set; }
        public string RankCode { get; set; }
        public string PayHouseCode { get; set; }
        public string RegionCode { get; set; }
        public string LOECode { get; set; }
        public string ProjectCode { get; set; }
        public string DesignationCode { get; set; }
        //public decimal BudgetSalary { get; set; }
        public decimal AgreedSalary { get; set; }
        public int Quantity { get; set; }
        public string Remarks { get; set; }
    }
    public class BudgetEntryAndEntryDetailsDto
    {
        public BudgetEntryDto BudgetEntryDto { get; set; }
        public IEnumerable<BudgetEntryDetailsDto> BudgetEntryDetails { get; set; }

        public List<IFormFile> Attachments { get; set; }
    }
    public class BudgetEntryAndEntryDetailsAttachmentDto
    {
        public string BudgetEntry { get; set; }
        public string BudgetEntryDetails { get; set; }
        
        public List<IFormFile> files { get; set; }
    }
}
