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
        Task InsertDetails(PRFDetailsMaint detail);

        Task InsertTransApproving(IEnumerable<TransApprovingPRF> transApprovingPRF);
        Task InsertAttachments(IEnumerable<PRFDetailsMaintAttachmentDto> attachments);
        Task<IEnumerable<GetPREntryDetails>> GetDetailsByPRFNo(string prfNo, string dbName);
        Task<PRFDetailsMaint> GetDetailById(long id);
        Task<IEnumerable<ViewPREntryWithStatusDto>> ViewPRFEntryByStatus(string status);
        Task<IEnumerable<ViewPREntryWithStatusDto>> ViewPREntryWithStatusWaitingDto();
        Task<IEnumerable<ViewPREntryWithStatusDto>> ViewPREntryWithStatusReturnedDto();
        Task Update(PRFHeaderMaint header);
        Task UpdatePRFNo(int identity, string prfNo);
        Task UpdateStatus(string prfNo, string status);
        Task UpdatePRDetailsStatus(string prfNo, string status);
        Task UpdateDetails(PRFDetailsMaint detail);
        Task UpdateTransApprovingStatus(UpdatePRFTransApprovingStatusDto dto);
        Task InsertComment(PRFMainReturnCommentDto comment);
        Task<IEnumerable<PRFMainReturnComment>> GetPRFHeaderMaintReturnCommentsByPRFNo(string prfNo);
    }
}
