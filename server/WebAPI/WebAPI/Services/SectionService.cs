﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.Section;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository repo;
        private readonly IAuditTrailService<Section> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        private const string TABLE_NAME = "tbl_fsSection";

        public SectionService(ISectionRepository repo,
             IAuditTrailService<Section> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var section = await repo.GetById(id);
            if (section != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Section(), section, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> DeleteByCode(string code)
        {
            var sec = await repo.GetByCode(code);
            if (sec != null)
            {
                await repo.DeleteByCode(code);

                await auditTrailService.Save(new Section(), sec, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Section sec)
        {
            // Get Company Code
            sec.CompanyCode = compInfoRepo.GetCompanyCode();
            if (String.IsNullOrEmpty(sec.SectionCode) || String.IsNullOrWhiteSpace(sec.SectionCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            var secData = await repo.GetByCode(sec.SectionCode);

            if (secData == null)
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
                    
                    // PAYROLL 
                    var result = await compInfoRepo.CheckTableIfExists(companyInfo.PayrollDB, TABLE_NAME);
                    if (result && companyInfo.PayrollFlag)
                    {
                        // Check from payroll database
                        var results = await repo.GetByCodeFromPayroll(sec.SectionCode, companyInfo.PayrollDB);
                        if (results == null)
                        {
                            // SAVE TO PAYROLL DB
                            await repo.InsertToPayrollFileSetUp(new SectionInsertToPayrollFSDto
                            {
                                SecCode = sec.SectionCode,
                                SecDesc = sec.SectionDesc,
                                SecHead = sec.SecHead,
                                AccountCode = "",
                                CreatedBy = "", // Current User,
                                DBName = companyInfo.PayrollDB
                            });
                        }

                    }

                    // TIME KEEPING
                    var tksResult = await compInfoRepo.CheckTableIfExists(companyInfo.TKSDB, TABLE_NAME);
                    if (tksResult && companyInfo.TKSFlag)
                    {
                        var results = await repo.GetByCodeFromTKS(sec.SectionCode, companyInfo.TKSDB);
                        if (results == null)
                        {
                            // SAVE TO TKS DB
                            await repo.InsertToTSKFileSetUp(new SectionInsertToTKSFSDto
                            {
                                SecCode = sec.SectionCode,
                                SecDesc = sec.SectionDesc,
                                SecHead = sec.SecHead,
                                SecHeadCode = sec.SecHeadCode,
                                DepCode = "",
                                DBName = companyInfo.TKSDB
                            });
                        }

                    }
                    // HRIS
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        var results = await repo.GetByCodeFromHRIS(sec.SectionCode, companyInfo.HRISDB);
                        if (results == null)
                        {
                            // SAVE TO HRIS DB
                            await repo.InsertToHRISFileSetUp(new SectionInsertToHRISFSDto
                            {
                                SecCode = sec.SectionCode,
                                SecDesc = sec.SectionDesc,
                                SecHead = sec.SecHead,
                                SecHeadCode = sec.SecHeadCode,
                                DBName = companyInfo.HRISDB,
                                DepCode = ""
                            });
                        }

                    }
                }


                await repo.InsertFileSetup(sec);
                await repo.Insert(sec);
                await auditTrailService.Save(new Section(), sec, "ADD");

                return CustomMessageHandler.RecordAdded();
            }
            return CustomMessageHandler.Error("Code is already used");
        }
        public async Task<CustomMessage> Update(Section sec)
        {
            var sectionData = await repo.GetByCode(sec.SectionCode);
            if (sectionData != null)
            {
                await repo.Update(sec);

                //Audit Trail
                await auditTrailService.Save(sectionData, sec, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
