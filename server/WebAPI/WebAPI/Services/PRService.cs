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

namespace WebAPI.Services
{
    public class PRService : IPRService
    {
        private readonly IPRRepository prRepo;
        private readonly IMapper mapper;
        public PRService(IPRRepository prRepo,
            IMapper mapper)
        {
            this.prRepo = prRepo;
            this.mapper = mapper;
        }

        public async Task<CustomMessage> Insert(PRFHeaderDetailsDto prfHeaderDetailsDto)
        {
            var identity = await prRepo.Insert(mapper.Map<PRFHeaderMaint>(prfHeaderDetailsDto.Header));

            if(identity == 0)
                return CustomMessageHandler.Error($"Error saving this record with identity {identity}");

            string prfNo = GenerateTransNo(identity); // Generate PRF No.

            await prRepo.UpdatePRFNo(identity, prfNo); // Update Main PRF No. after Insert

            await prRepo.InsertDetails(mapper.Map<IEnumerable<PRFDetailsMaint>>(prfHeaderDetailsDto.Details)); // Insert to PRF Details

            return CustomMessageHandler.Success($"Personnel Requisition Entry has been successfully submitted . Your Transaction No. is {prfNo}");
        }

        private string GenerateTransNo(int identity) => string.Format("PR{0}", identity.ToString("D12"));
    }
}
