using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
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
        private readonly IFileSetupService fileSetupService;
        private readonly IMapper mapper;
        public MajorService(IMajorRepository repo,
             IAuditTrailService<Major> auditTrailService,
             ICompanyInformationRepository compInfoRepo,
             IFileSetupService fileSetupService,
             IMapper mapper)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
            this.fileSetupService = fileSetupService;
            this.mapper = mapper;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var major = await repo.GetById(id);
            if (major != null)
            {
               // await repo.Delete(id);

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
                var companyInfo = await compInfoRepo.GetByCompanyCode(compInfoRepo.GetCompanyCode());
                var validationResult =
                    fileSetupService.Validate(companyInfo, await compInfoRepo.CheckDBIfExists(companyInfo.PayrollDB),
                        await compInfoRepo.CheckDBIfExists(companyInfo.TKSDB), await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB));

                if (!validationResult.hasError)
                {
                    return CustomMessageHandler.Error(validationResult.message);
                }

                // HRIS
                var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                if (hrisResult && companyInfo.HRISFlag)
                {
                    var results = await repo.GetByCodeFromHRIS(major.MajorCode, companyInfo.HRISDB);
                    if (results != null)
                    {
                        // SAVE TO HRIS DB
                        await repo.DeleteFromHRISFileSetUp(new DeleteSetUpDto
                        {
                            Code = major.MajorCode,
                            DBName = companyInfo.HRISDB
                        });
                    }
                }

                await repo.DeleteFileSetUp(major.MajorCode);
                await repo.Delete(major.MajorCode);


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
                var validationResult =
                    fileSetupService.Validate(companyInfo, await compInfoRepo.CheckDBIfExists(companyInfo.PayrollDB),
                        await compInfoRepo.CheckDBIfExists(companyInfo.TKSDB), await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB));

                if (!validationResult.hasError)
                {
                    return CustomMessageHandler.Error(validationResult.message);
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
            var majData = await repo.GetByCode(maj.MajorCode);
            if (majData != null)
            {
                var companyInfo = await compInfoRepo.GetByCompanyCode(compInfoRepo.GetCompanyCode());
                var validationResult =
                    fileSetupService.Validate(companyInfo, await compInfoRepo.CheckDBIfExists(companyInfo.PayrollDB),
                        await compInfoRepo.CheckDBIfExists(companyInfo.TKSDB), await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB));

                if (!validationResult.hasError)
                {
                    return CustomMessageHandler.Error(validationResult.message);
                }
                else
                {
                    // HRIS 
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromHRIS(maj.MajorCode, companyInfo.HRISDB);
                        if (results != null)
                        {
                            var majDto = mapper.Map<MajorUpdateToHRISFSDto>(maj);
                            majDto.DBName = companyInfo.HRISDB;
                            // SAVE TO HRIS DB
                            await repo.UpdateToHRISFileSetUp(majDto);
                        }
                    }

                    await repo.UpdateFileSetup(maj);
                    await repo.Update(maj);

                    //Audit Trail
                    await auditTrailService.Save(majData, maj, "EDIT");

                    return CustomMessageHandler.RecordUpdated();
                }
            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
