using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.PRDto;
using WebAPI.Helpers;
using WebAPI.Models.PRModel;

namespace WebAPI.ServiceInterfaces
{
    public interface IPRService
    {
        Task<CustomMessage> Insert(PRFHeaderDetailsDto prfHeaderDetailsDto);
        Task<CustomMessage> InsertReturnPRF(InsertReturnPRFDto dto);
        Task<GetPREntryApprovalDetailsDto> GetPREntryApprovalDetails(string prfNo);
        Task<CustomMessage> AcceptEntry(string prfNo);
        Task<CustomMessage> DeclineEntry(string prfNo);
        Task<CustomMessage> InsertNotBudgeted(PRFHeaderDetailsDto dto);
        Task<CustomMessage> ReturnEntry(PRFMainReturnCommentDto comment);
        Task<IEnumerable<GetPREntryDetails>> GetPREntryMaintDetailsByPRFNo(string prfNo);

        // PRF Extend
        Task<CustomMessage> PRFExtendApproved(int id);
        Task<CustomMessage> PRFExtendDeclined(int id);
        Task<CustomMessage> InsertPRFExtend(PRFExtend prfExtend);
        Task<bool> GetPRFExtendByPRFNo(string prfNo);
        Task<IEnumerable<PRFExtend>> GetPRFExtendByStatus(string status);
    }
}
