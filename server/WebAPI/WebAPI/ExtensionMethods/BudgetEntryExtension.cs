using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Models.BudgetEntryModel;

namespace WebAPI.ExtensionMethods
{
    public static class BudgetEntryExtension
    {
        public static BudgetEntryMainHeader MapToBudgetEntryMainHeader(this BudgetEntryDto dto)
        {
            return new BudgetEntryMainHeader
            {
                DateRequired = dto.DateRequired,
                DurationFrom = dto.DurationFrom,
                DurationTo = dto.DurationTo,
                Description = dto.Description,
                Reason = dto.Reason,
                EmpStatus = dto.EmpStatus,
                Year = dto.DateRequired.ToString("yyyy"),
                Month = dto.DateRequired.ToString("MMMM"),
                Status = "WAITING",
                Remarks = dto.Remarks,
                
            };
        }
        public static IEnumerable<BudgetEntryMaintDetails> MapToBudgetEntryMaintDetails(this IEnumerable<BudgetEntryDetailsDto> dto, string transNo)
        {
            List<BudgetEntryMaintDetails> budgetEntryMaintDetails = new List<BudgetEntryMaintDetails>();
            foreach (var item in dto)
            {
                var detail = new BudgetEntryMaintDetails
                {
                    AgreedSalary = item.AgreedSalary,
                    AreaCode = item.AreaCode,
                    BraCode = item.BraCode,
                    DatabaseCode = item.DatabaseCode,
                    DepCode = item.DepCode,
                    DivisionCode = item.DivisionCode,
                    GrpCode = item.GroupCode,
                    LocCode = item.LocationCode,
                    LocId = item.LocId,
                    PayHouseCode = item.PayHouseCode,
                    PositionCode = item.DesignationCode,
                    ProjectCode = item.ProjectCode,
                    Quantity = item.Quantity,
                    RankCode = item.RankCode,
                    RegionCode = item.RegionCode,
                    SectionCode = item.SectionCode,
                    UnitCode = item.UnitCode,
                    TransactionNo = transNo
                };
                budgetEntryMaintDetails.Add(detail);
            }

            return budgetEntryMaintDetails;
        }
    }
}
