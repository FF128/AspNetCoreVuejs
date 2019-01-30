using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.MajorDto;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class MajorService : IMajorService
    {
        private const string TABLE_NAME = "tbl_fsMajor";
        private readonly IMajorRepository repo;
        private readonly IAuditTrailService<Major> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IMapper mapper;
        public MajorService(IMajorRepository repo,
             IAuditTrailService<Major> auditTrailService,
             ICompanyInformationRepository compInfoRepo,
             IMapper mapper)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
            this.mapper = mapper;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var major = await repo.GetById(id);
            if (major != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Major(), major, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> DeleteByCode(string code)
        {
            var major = await repo.GetByCode(code);
            if (major != null)
            {
                await repo.DeleteByCode(code);

                await auditTrailService.Save(new Major(), major, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Major maj)
        {
            // Get Company Code
            maj.CompanyCode = compInfoRepo.GetCompanyCode();
            if (String.IsNullOrEmpty(maj.MajorCode) || String.IsNullOrWhiteSpace(maj.MajorCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            var majData = await repo.GetByCode(maj.MajorCode);

            if (majData == null)
            {
                var companyInfo = await compInfoRepo.GetByCompanyCode(compInfoRepo.GetCompanyCode());
                if (companyInfo == null)
                {
                    return CustomMessageHandler.Error("Company Information doesn't exist");
                }

                if (!(await compInfoRepo.CheckDBIfExists(companyInfo.PayrollDB)))
                {
                    return CustomMessageHandler.Error("Payroll Database doesn't exist!");
                }
                else if (!(await compInfoRepo.CheckDBIfExists(companyInfo.TKSDB)))
                {
                    return CustomMessageHandler.Error("Timekeeping Database doesn't exist!");
                }
                else if (!(await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB)))
                {
                    return CustomMessageHandler.Error("HRIS Database doesn't exist!");
                }
                else
                {
                    // HRIS
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        var results = await repo.GetByCodeFromHRIS(maj.MajorCode, companyInfo.HRISDB);
                        if (results == null)
                        {
                            var courseDto = mapper.Map<MajorInsertToHRISFSDto>(maj);
                            courseDto.DBName = companyInfo.HRISDB;
                            // SAVE TO HRIS DB
                            await repo.InsertToHRISFileSetUp(courseDto);
                        }

                    }
                }


                await repo.InsertFileSetup(maj);
                await repo.Insert(maj);
                await auditTrailService.Save(new Major(), maj, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Major maj)
        {
            var major = await repo.GetByCode(maj.MajorCode);
            if (major != null)
            {
                await repo.Update(maj);

                //Audit Trail
                await auditTrailService.Save(major, maj, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
