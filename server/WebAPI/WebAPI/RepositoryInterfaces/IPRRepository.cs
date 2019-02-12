using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.PRDto;
using WebAPI.Models.PRModel;

namespace WebAPI.RepositoryInterfaces
{
    public interface IPRRepository
    {
        Task<int> Insert(PRFHeaderMaint pr);
        Task<PRFHeaderMaint> GetByPRFNo(string prfNo);
        Task InsertDetails(IEnumerable<PRFDetailsMaint> details);
        Task InsertTransApproving(TransApprovingPRF transApprovingPRF);
        Task InsertAttachments(IEnumerable<PRFDetailsMaintAttachmentDto> attachments);
        Task<IEnumerable<GetPREntryDetails>> GetDetailsByPRFNo(string prfNo, string dbName);
        Task<IEnumerable<ViewPREntryWithStatusDto>> ViewPREntryWithStatusWaitingDto();
        Task<IEnumerable<ViewPREntryWithStatusDto>> ViewPREntryWithStatusReturnedDto();
        Task UpdatePRFNo(int identity, string prfNo);
        Task UpdateStatus(string prfNo, string status);
        Task UpdatePRDetailsStatus(string prfNo, string status);
       
    }
}
