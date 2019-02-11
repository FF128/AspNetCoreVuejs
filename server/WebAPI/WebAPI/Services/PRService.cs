using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.PRDto;
using WebAPI.Helpers;
using WebAPI.Models.PRModel;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;
using WebAPI.ExtensionMethods;

namespace WebAPI.Services
{
    public class PRService : IPRService
    {
        private readonly IPRRepository prRepo;
        private readonly IMapper mapper;
        private readonly ICompanyInformationRepository compInfoRepo;
        public PRService(IPRRepository prRepo,
            ICompanyInformationRepository compInfoRepo,
            IMapper mapper)
        {
            this.prRepo = prRepo;
            this.mapper = mapper;
            this.compInfoRepo = compInfoRepo;
        }
        
        public async Task<GetPREntryApprovalDetailsDto> GetPREntryApprovalDetails(string prfNo)
        {
            return new GetPREntryApprovalDetailsDto
            {
                Header = mapper.Map<PRFHeaderMaintDto>(await prRepo.GetByPRFNo(prfNo)),
                Details = await prRepo.GetDetailsByPRFNo(prfNo)
            };
        }

        public async Task<CustomMessage> Insert(PRFHeaderDetailsDto prfHeaderDetailsDto)
        {
            prfHeaderDetailsDto.Header.CompanyCode = compInfoRepo.GetCompanyCode();

            var identity = await prRepo.Insert(mapper.Map<PRFHeaderMaint>(prfHeaderDetailsDto.Header));

            if(identity == 0)
                return CustomMessageHandler.Error($"Error saving this record with identity {identity}");

            string prfNo = GenerateTransNo(identity); // Generate PRF No.

            await prRepo.UpdatePRFNo(identity, prfNo); // Update Main PRF No. after Insert

            var details = mapper.Map<IEnumerable<PRFDetailsMaint>>(prfHeaderDetailsDto.Details);

            await prRepo.InsertDetails(details.MapToPRFDetailsMaint(prfNo, prfHeaderDetailsDto.IsBudgeted)); // Insert to PRF Details

            return CustomMessageHandler.Success($"Personnel Requisition Entry has been successfully submitted . Your Transaction No. is {prfNo}");
        }

        public async Task<CustomMessage> AcceptEntry(string prfNo)
        {
            var detail = await prRepo.GetDetailsByPRFNo(prfNo);
            if (detail == null)
            {
                return CustomMessageHandler.Error($"Unable to find any data using this transaction no. {prfNo}");
            }

            await prRepo.UpdatePRDetailsStatus(prfNo, "DONE");

            await prRepo.UpdateStatus(prfNo, "OPEN");

            return CustomMessageHandler.RecordUpdated();
        }

        private string GenerateTransNo(int identity) => string.Format("PR{0}", identity.ToString("D12"));

        public async Task<CustomMessage> DeclineEntry(string prfNo)
        {
            var detail = await prRepo.GetDetailsByPRFNo(prfNo);
            if (detail == null)
            {
                return CustomMessageHandler.Error($"Unable to find any data using this transaction no. {prfNo}");
            }

            await prRepo.UpdateStatus(prfNo, "CLOSE");

            return CustomMessageHandler.RecordUpdated();

        }
    }
}
