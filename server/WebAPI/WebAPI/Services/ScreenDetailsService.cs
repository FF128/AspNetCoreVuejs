using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class ScreenDetailsService : IScreenDetailsService
    {
        private readonly IScreenDetailsRepository repo;
        private readonly IAuditTrailService<ScreenDetails> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
     
        public ScreenDetailsService(IScreenDetailsRepository repo,
            IAuditTrailService<ScreenDetails> auditTrailService,
            ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
        }
        public async Task<CustomMessage> Delete(int id)
        {
            var screenDetail = await repo.GetById(id);
            if(screenDetail == null)
            {
                return CustomMessageHandler.Error("Record doesn't exist!");
            }

            await repo.Delete(id);
            await repo.DeleteScreenDetailsSub(screenDetail.Id);

            await auditTrailService.Save(new ScreenDetails(), screenDetail, "DELETE");

            return CustomMessageHandler.RecordDeleted();
        }

        public async Task<CustomMessage> Insert(ScreenDetails sd)
        {
            if (String.IsNullOrEmpty(sd.ScreenDetailsCode) || String.IsNullOrWhiteSpace(sd.ScreenDetailsCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(sd.ScreenDetailsCode)) == null)
            {
                sd.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(sd);

                await auditTrailService.Save(new ScreenDetails(), sd, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> InsertScreenDetailSub(AddScreenDetailSubDto dto)
        {
            var screenDetail = await repo.GetById(dto.Id);
            if(screenDetail == null)
            {
                return CustomMessageHandler.Error("Screen Detail doesn't exist");
            }

            // Delete
            await repo.DeleteScreenDetailsSub(dto.Id);

            //Insert
            dto.ScreenDetailsSubs.ToList().ForEach(x =>
            {
                x.ScreenDetailId = dto.Id;
                x.CompanyCode = compInfoRepo.GetCompanyCode();
            });

            foreach (var sub in dto.ScreenDetailsSubs)
            {
                await repo.InsertScreenDetailsSub(sub);
            }
            return CustomMessageHandler.RecordAdded();
        }

        public async Task<CustomMessage> Update(ScreenDetails sd)
        {
            var screeDetails = await repo.GetByCode(sd.ScreenDetailsCode);
            if (screeDetails != null)
            {
                await repo.Update(sd);

                //Audit Trail
                await auditTrailService.Save(screeDetails, sd, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
