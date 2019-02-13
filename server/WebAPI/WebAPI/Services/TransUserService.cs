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
    public class TransUserService : ITransUserService
    {
        private readonly ITransUserRepository repo;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IUserRepository userRepo;
        public TransUserService(ITransUserRepository repo,
            ICompanyInformationRepository compInfoRepo,
            IUserRepository userRepo)
        {
            this.repo = repo;
            this.compInfoRepo = compInfoRepo;
            this.userRepo = userRepo;
        }
        public async Task<CustomMessage> Insert(InsertTransUserDto dto)
        {
            var companyCode = compInfoRepo.GetCompanyCode();
            dto.TransUser.CreatedBy = userRepo.GetEmpCode();
            dto.TransUser.IsActive = true;
            dto.TransUser.CompanyCode = companyCode;
            var identity = await repo.Insert(dto.TransUser);
            if (identity == 0)
                throw new Exception($"Invalid TransID ({identity})");

            dto.TransUserDepartments.ToList().ForEach(dep =>
            {
                dep.CompanyCode = companyCode;
                dep.TransID = identity;
            });

            dto.TransUserPaylocations.ToList().ForEach(loc =>
            {
                loc.CompanyCode = companyCode;
                loc.TransID = identity;
            });
            await repo.InserTransUserPayLoc(dto.TransUserPaylocations);
            await repo.InsertTransUserDepartment(dto.TransUserDepartments);

            return CustomMessageHandler.RecordAdded();
        }
    }
}
