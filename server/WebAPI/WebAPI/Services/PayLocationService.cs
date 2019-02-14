using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Helpers;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class PayLocationService : IPayLocationService
    {
        private readonly IPayLocationRepository repo;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IFileSetupService fileSetupService;
        public PayLocationService(IPayLocationRepository repo,
            ICompanyInformationRepository compInfoRepo,
            IFileSetupService fileSetupService)
        {
            this.repo = repo;
            this.fileSetupService = fileSetupService;
            this.compInfoRepo = compInfoRepo;
        }

        public async Task<PayLocationDto> GetPayLocationById(long locId)
        {
            var companyInfo = await compInfoRepo.GetByCompanyCode(compInfoRepo.GetCompanyCode());
            var validationResult =
                fileSetupService.Validate(companyInfo, await compInfoRepo.CheckDBIfExists(companyInfo.PayrollDB),
                    await compInfoRepo.CheckDBIfExists(companyInfo.TKSDB), await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB));

            if (!validationResult.hasError)
            {
                throw new Exception(validationResult.message);
            }

            return await repo.GetPayLocationById(locId,companyInfo.HRISDB);
        }

        public async Task<IEnumerable<PayLocationDto>> GetPayLocations()
        {
            var companyInfo = await compInfoRepo.GetByCompanyCode(compInfoRepo.GetCompanyCode());
            var validationResult =
                fileSetupService.Validate(companyInfo, await compInfoRepo.CheckDBIfExists(companyInfo.PayrollDB),
                    await compInfoRepo.CheckDBIfExists(companyInfo.TKSDB), await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB));

            if (!validationResult.hasError)
            {
                return new List<PayLocationDto>();
            }

            return await repo.GetPayLocations(companyInfo.HRISDB);
        }
    }
}
