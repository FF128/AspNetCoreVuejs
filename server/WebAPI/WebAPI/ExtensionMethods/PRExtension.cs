using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.PRModel;

namespace WebAPI.ExtensionMethods
{
    public static class PRExtension
    {
        public static IEnumerable<PRFDetailsMaint> MapToPRFDetailsMaint( this IEnumerable<PRFDetailsMaint> pRFDetails,string prfNo, bool isBudgeted)
        {
            List<PRFDetailsMaint> budgetEntryMaintDetails = new List<PRFDetailsMaint>();
            foreach (var item in pRFDetails)
            {
                var detail = new PRFDetailsMaint
                {
                    AgreedSalary = item.AgreedSalary,
                    AreaCode = item.AreaCode,
                    BraCode = item.BraCode,
                    DatabaseCode = item.DatabaseCode,
                    DepCode = item.DepCode,
                    DivisionCode = item.DivisionCode,
                    GrpCode = item.GrpCode,
                    LocCode = item.LocCode,
                    LocId = item.LocId,
                    PayHouseCode = item.PayHouseCode,
                    DesignationCode = item.DesignationCode,
                    ProjectCode = item.ProjectCode,
                    RankCode = item.RankCode,
                    RegionCode = item.RegionCode,
                    SectionCode = item.SectionCode,
                    UnitCode = item.UnitCode,
                    Quantity = item.Quantity,
                    BudgetDetailsID = item.BudgetDetailsID,
                    BudgetTransNo = item.BudgetTransNo,
                    PRFNo = prfNo,
                    Status = "WAITING",
                    IsBudgeted = isBudgeted
                };
                budgetEntryMaintDetails.Add(detail);
            }

            return budgetEntryMaintDetails;

        }
    }
}
