using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.Religion;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class ReligionService : IReligionService
    {
        private const string TABLE_NAME = "tbl_fsReligion";
        private readonly IReligionRepository repo;
        private readonly IAuditTrailService<Religion> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IFileSetupService fileSetupService;
        public ReligionService(IReligionRepository repo,
            IAuditTrailService<Religion> auditTrailService,
            ICompanyInformationRepository compInfoRepo,
            IFileSetupService fileSetupService)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
            this.fileSetupService = fileSetupService;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var rel = await repo.GetById(id);
            if (rel != null)
            {
                await repo.Delete(id);

                //Audit Trail
                await auditTrailService.Save(new Religion(), rel, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> DeleteByCode(string code)
        {
            var rel = await repo.GetByCode(code);
            if (rel != null)
            {
                await repo.DeleteByCode(code);

                await auditTrailService.Save(new Religion(), rel, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Religion rel)
        {
            // Get Company Code
            rel.CompanyCode = compInfoRepo.GetCompanyCode();
            if (String.IsNullOrEmpty(rel.Code) || String.IsNullOrWhiteSpace(rel.Code))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            var relData = await repo.GetByCode(rel.Code);

            if (relData == null)
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
                        var results = await repo.GetByCodeFromHRIS(rel.Code, companyInfo.HRISDB);
                        if (results == null)
                        {
                            // SAVE TO HRIS DB
                            await repo.InsertToHRISFileSetUp(new ReligionInsertToHRISFSDto
                            {
                                RelCode = rel.Code,
                                RelDesc = rel.Description,
                                CreatedBy = "", // Current User
                                DBName = companyInfo.HRISDB
                            });
                        }

                    }
                }


                await repo.InsertFileSetup(rel);
                await repo.Insert(rel);
                await auditTrailService.Save(new Religion(), rel, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Religion rel)
        {
            var relData = await repo.GetByCode(rel.Code);
            if (relData != null)
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
                        var results = await repo.GetByCodeFromHRIS(rel.Code, companyInfo.HRISDB);
                        if (results != null)
                        {
                            // SAVE TO PAYROLL DB
                            await repo.UpdateToHRISFileSetUp(new ReligionUpdateToHRISFSDto
                            {
                                RelCode = rel.Code,
                                RelDesc = rel.Description,
                                EditedBy = "", // Current User
                                DBName = companyInfo.HRISDB
                            });
                        }
                    }

                    await repo.UpdateFileSetup(rel);
                    await repo.Update(rel);

                    //Audit Trail
                    await auditTrailService.Save(relData, rel, "EDIT");

                    return CustomMessageHandler.RecordUpdated();
                }
            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
