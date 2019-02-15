using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.PRDto;
using WebAPI.Helpers;

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
    }
}
