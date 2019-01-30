using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.Location;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class LocationService : ILocationService
    {
        private const string TABLE_NAME = "tbl_fsLocation";
        private readonly ILocationRepository repo;
        private readonly IAuditTrailService<Location> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IFileSetupService fileSetupService;
        public LocationService(ILocationRepository repo,
             IAuditTrailService<Location> auditTrailService,
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
            var location = await repo.GetById(id);
            if (location != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Location(), location, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> DeleteByCode(string code)
        {
            var loc = await repo.GetByCode(code);
            if (loc != null)
            {
                await repo.DeleteByCode(code);

                await auditTrailService.Save(new Location(), loc, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Location loc)
        {
            // Get Company Code
            loc.CompanyCode = compInfoRepo.GetCompanyCode();
            if (String.IsNullOrEmpty(loc.LocationCode) || String.IsNullOrWhiteSpace(loc.LocationCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            var locData = await repo.GetByCode(loc.LocationCode);

            if (locData == null)
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
                    var locInsertDto = new LocationInsertFileSetupDto
                    {
                        LocationCode = loc.LocationCode,
                        LocationDesc = loc.LocationDesc,
                        AcctCode = loc.AcctCode,
                        HeadCode = loc.HeadCode,
                        Head = loc.Head,
                        CreatedBy = "" // Current User
                    };
                    // PAYROLL 
                    var result = await compInfoRepo.CheckTableIfExists(companyInfo.PayrollDB, TABLE_NAME);
                    if (result && companyInfo.PayrollFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromPayroll(loc.LocationCode, companyInfo.PayrollDB);
                        if (results == null)
                        {
                            locInsertDto.DBName = companyInfo.PayrollDB;
                            // SAVE TO PAYROLL DB
                            await repo.InsertToPayrollFileSetUp(locInsertDto);
                        }

                    }

                    // TIME KEEPING
                    var tksResult = await compInfoRepo.CheckTableIfExists(companyInfo.TKSDB, TABLE_NAME);
                    if (tksResult && companyInfo.TKSFlag)
                    {
                        var results = await repo.GetByCodeFromTKS(loc.LocationCode, companyInfo.TKSDB);
                        if (results == null)
                        {
                            locInsertDto.DBName = companyInfo.TKSDB;
                            // SAVE TO TKS DB
                            await repo.InsertToTKSFileSetUp(locInsertDto);
                        }

                    }
                    // HRIS
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        var results = await repo.GetByCodeFromHRIS(loc.LocationCode, companyInfo.HRISDB);
                        if (results == null)
                        {
                            locInsertDto.DBName = companyInfo.HRISDB;
                            // SAVE TO HRIS DB
                            await repo.InsertToHRISFileSetUp(locInsertDto);
                        }

                    }
                }


                await repo.InsertFileSetup(loc);
                await repo.Insert(loc);
                await auditTrailService.Save(new Location(), loc, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Location loc)
        {
            var locData = await repo.GetByCode(loc.LocationCode);
            if (locData != null)
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
                    var locUpdateDto = new LocationUpdateFileSetupDto
                    {
                        LocationCode = loc.LocationCode,
                        LocationDesc = loc.LocationDesc,
                        AcctCode = loc.AcctCode,
                        HeadCode = loc.HeadCode,
                        Head = loc.Head,
                        EditedBy = "" // Current User
                    };
                    // PAYROLL 
                    var result = await compInfoRepo.CheckTableIfExists(companyInfo.PayrollDB, TABLE_NAME);
                    if (result && companyInfo.PayrollFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromPayroll(loc.LocationCode, companyInfo.PayrollDB);
                        if (results != null)
                        {
                            locUpdateDto.DBName = companyInfo.PayrollDB;
                            // SAVE TO PAYROLL DB
                            await repo.UpdateToPayrollFileSetUp(locUpdateDto);
                        }
                    }
                    // TIME KEEPING
                    var tksResult = await compInfoRepo.CheckTableIfExists(companyInfo.TKSDB, TABLE_NAME);
                    if (tksResult && companyInfo.TKSFlag)
                    {
                        var results = await repo.GetByCodeFromTKS(loc.LocationCode, companyInfo.TKSDB);
                        if (results != null)
                        {
                            locUpdateDto.DBName = companyInfo.TKSDB;
                            // SAVE TO TKS DB
                            await repo.UpdateToTKSFileSetUp(locUpdateDto);
                        }
                    }
                    // HRIS 
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromHRIS(loc.LocationCode, companyInfo.HRISDB);
                        if (results != null)
                        {
                            locUpdateDto.DBName = companyInfo.HRISDB;
                            // SAVE TO PAYROLL DB
                            await repo.UpdateToHRISFileSetUp(locUpdateDto);
                        }
                    }

                    await repo.UpdateFileSetup(loc);
                    await repo.Update(loc);

                    //Audit Trail
                    await auditTrailService.Save(locData, loc, "EDIT");

                    return CustomMessageHandler.RecordUpdated();
                }
            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
