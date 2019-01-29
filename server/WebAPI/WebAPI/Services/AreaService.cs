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
    public class AreaService : IAreaService
    {
        private const string TABLE_NAME = "tbl_fsArea";
        private readonly IAreaRepository repo;
        private readonly IAuditTrailService<Area> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public AreaService(IAreaRepository repo,
             IAuditTrailService<Area> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var Area = await repo.GetById(id);
            if (Area != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Area(), Area, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }
        public async Task<CustomMessage> DeleteByCode(string code)
        {
            var area = await repo.GetByCode(code);
            if (area != null)
            {
                await repo.DeleteByCode(code);

                await auditTrailService.Save(new Area(), area, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }
        public async Task<CustomMessage> Insert(Area area)
        {
            // Get Company Code
            area.CompanyCode = compInfoRepo.GetCompanyCode();
            if (String.IsNullOrEmpty(area.AreaCode) || String.IsNullOrWhiteSpace(area.AreaCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            var areaData = await repo.GetByCode(area.AreaCode);

            if (areaData == null)
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
                    var areaInsertDto = new AreaInsertToFileSetUpDto
                    {
                        AreaCode = area.AreaCode,
                        AreaDesc = area.AreaDesc,
                        AcctCode = area.AcctCode,
                        HeadCode = area.HeadCode,
                        CreatedBy = "" // Current User
                    };
                    // PAYROLL 
                    var result = await compInfoRepo.CheckTableIfExists(companyInfo.PayrollDB, TABLE_NAME);
                    if (result && companyInfo.PayrollFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromPayroll(area.AreaCode, companyInfo.PayrollDB);
                        if (results == null)
                        {
                            areaInsertDto.DBName = companyInfo.PayrollDB;
                            // SAVE TO PAYROLL DB
                            await repo.InsertToPayrollFileSetUp(areaInsertDto);
                        }

                    }

                    // TIME KEEPING
                    var tksResult = await compInfoRepo.CheckTableIfExists(companyInfo.TKSDB, TABLE_NAME);
                    if (tksResult && companyInfo.TKSFlag)
                    {
                        var results = await repo.GetByCodeFromTKS(area.AreaCode, companyInfo.TKSDB);
                        if(results == null)
                        {
                            areaInsertDto.DBName = companyInfo.TKSDB;
                            // SAVE TO TKS DB
                            await repo.InsertToTSKFileSetUp(areaInsertDto);
                        } 
                        
                    }
                    // HRIS
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        var results = await repo.GetByCodeFromHRIS(area.AreaCode, companyInfo.HRISDB);
                        if (results == null)
                        {
                            areaInsertDto.DBName = companyInfo.HRISDB;
                            // SAVE TO HRIS DB
                            await repo.InsertToHRISFileSetUp(areaInsertDto);
                        }

                    }
                }
               

                await repo.InsertFileSetup(area);
                await repo.Insert(area);
                await auditTrailService.Save(new Area(), area, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Area area)
        {
            var AreaData = await repo.GetByCode(area.AreaCode);
            if (AreaData != null)
            {
                await repo.Update(area);

                //Audit Trail
                await auditTrailService.Save(AreaData, area, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
