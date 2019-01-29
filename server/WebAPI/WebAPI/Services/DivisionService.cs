using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.Division;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class DivisionService : IDivisionService
    {
        private const string TABLE_NAME = "tbl_fsDivision";
        private readonly IDivisionRepository repo;
        private readonly IAuditTrailService<Division> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public DivisionService(IDivisionRepository repo,
             IAuditTrailService<Division> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var division = await repo.GetById(id);
            if (division != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Division(), division, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> DeleteByCode(string code)
        {
            var div = await repo.GetByCode(code);
            if (div != null)
            {
                await repo.DeleteByCode(code);

                await auditTrailService.Save(new Division(), div, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Division div)
        {
            // Get Company Code
            div.CompanyCode = compInfoRepo.GetCompanyCode();
            if (String.IsNullOrEmpty(div.DivisionCode) || String.IsNullOrWhiteSpace(div.DivisionCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            var divData = await repo.GetByCode(div.DivisionCode);

            if (divData == null)
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
           

                    // TIME KEEPING
                    var tksResult = await compInfoRepo.CheckTableIfExists(companyInfo.TKSDB, TABLE_NAME);
                    if (tksResult && companyInfo.TKSFlag)
                    {
                        var results = await repo.GetByCodeFromTKS(div.DivisionCode, companyInfo.TKSDB);
                        if (results == null)
                        {
                            // SAVE TO TKS DB
                            await repo.InsertToTSKFileSetUp(new DivInsertToTKSFSDto
                            {
                                DivCode = div.DivisionCode,
                                DivDesc = div.DivisionDesc,
                                DivHead = div.Head,
                                DivHeadCode = div.HeadCode,
                                DBName = companyInfo.TKSDB
                            });
                        }

                    }
                    // HRIS
                    var hrisResult = await compInfoRepo.CheckTableIfExists(companyInfo.HRISDB, TABLE_NAME);
                    if (hrisResult && companyInfo.HRISFlag)
                    {
                        var results = await repo.GetByCodeFromHRIS(div.DivisionCode, companyInfo.HRISDB);
                        if (results == null)
                        {
                            // SAVE TO HRIS DB
                            await repo.InsertToHRISFileSetUp(new DivInsertToHRISFSDto
                            {
                                DivCode = div.DivisionCode,
                                DivDesc = div.DivisionDesc,
                                DivHead = div.Head,
                                DivHeadCode = div.HeadCode,
                                DBName = companyInfo.HRISDB,
                                CreatedBy = "" // Current User
                            });
                        }

                    }
                }


                await repo.InsertFileSetup(new DivInsertFileSetupDto
                {
                    DivCode = div.DivisionCode,
                    DivDesc = div.DivisionDesc,
                    Head = div.Head,
                    HeadCode = div.HeadCode,
                    CreatedBy = "" // Current User
                });
                await repo.Insert(div);
                await auditTrailService.Save(new Division(), div, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Division div)
        {
            var divisionData = await repo.GetByCode(div.DivisionCode);
            if (divisionData != null)
            {
                await repo.Update(div);

                //Audit Trail
                await auditTrailService.Save(divisionData, div, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
