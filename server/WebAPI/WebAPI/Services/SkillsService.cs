using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Dtos.SkillsDto;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class SkillsService : ISkillsService
    {
        private const string TABLE_NAME = "tbl_fsSkills";
        private readonly ISkillsRepository repo;
        private readonly IAuditTrailService<Skills> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IFileSetupService fileSetupService;
        private readonly IMapper mapper;
        public SkillsService(ISkillsRepository repo,
             IAuditTrailService<Skills> auditTrailService,
             ICompanyInformationRepository compInfoRepo,
             IFileSetupService fileSetupService,
             IMapper mapper)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
            this.mapper = mapper;
            this.fileSetupService = fileSetupService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var skillData = await repo.GetById(id);
            if (skillData != null)
            {
                //await repo.Delete(id);

                await auditTrailService.Save(new Skills(), skillData, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> DeleteByCode(string code)
        {
            var skill = await repo.GetByCode(code);
            if (skill != null)
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
                    var results = await repo.GetByCodeFromHRIS(skill.SkillsCode, companyInfo.HRISDB);
                    if (results != null)
                    {
                        // SAVE TO HRIS DB
                        await repo.DeleteFromHRISFileSetUp(new DeleteSetUpDto
                        {
                            Code = skill.SkillsCode,
                            DBName = companyInfo.HRISDB
                        });
                    }
                }

                await repo.DeleteFileSetUp(skill.SkillsCode);
                await repo.Delete(skill.SkillsCode);


                await auditTrailService.Save(new Skills(), skill, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Skills skill)
        {
            // Get Company Code
            skill.CompanyCode = compInfoRepo.GetCompanyCode();
            if (String.IsNullOrEmpty(skill.SkillsCode) || String.IsNullOrWhiteSpace(skill.SkillsCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            var skillData = await repo.GetByCode(skill.SkillsCode);

            if (skillData == null)
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
                        var results = await repo.GetByCodeFromHRIS(skill.SkillsCode, companyInfo.HRISDB);
                        if (results == null)
                        {
                            var skillDto = mapper.Map<SkillInsertToHRISFSDto>(skill);
                            skillDto.DBName = companyInfo.HRISDB;
                            // SAVE TO HRIS DB
                            await repo.InsertToHRISFileSetUp(skillDto);
                        }

                    }
                }


                await repo.InsertFileSetup(skill);
                await repo.Insert(skill);
                await auditTrailService.Save(new Skills(), skill, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Skills skill)
        {
            var skillData = await repo.GetByCode(skill.SkillsCode);
            if (skillData != null)
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
                        var results = await repo.GetByCodeFromHRIS(skill.SkillsCode, companyInfo.HRISDB);
                        if (results != null)
                        {
                            var skillDto = mapper.Map<SkillUpdateToHRISFSDto>(skill);
                            skillDto.DBName = companyInfo.HRISDB;
                            // SAVE TO HRIS DB
                            await repo.UpdateToHRISFileSetUp(skillDto);
                        }
                    }

                    await repo.UpdateFileSetup(skill);
                    await repo.Update(skill);

                    //Audit Trail
                    await auditTrailService.Save(skillData, skill, "EDIT");

                    return CustomMessageHandler.RecordUpdated();
                }
            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
