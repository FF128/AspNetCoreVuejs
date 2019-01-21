using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using WebAPI.Dtos;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class CompanyInfoService : ICompanyInfoService
    {
        private readonly ICompanyInformationRepository repo;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        public CompanyInfoService(ICompanyInformationRepository repo,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }
        private byte[] ConvertFileToByte(IFormFile file)
        {
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    return fileBytes;
                    //string s = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                }
            }
            return null;
        }

        public async Task<CustomMessage> InsertOrUpdate(CompanyInformation info)
        {
            var companyInfo = await repo.GetByCompanyCode(info.Code);
            if (companyInfo == null)
            {
                // Insert
                if (!(await repo.CheckDBIfExists(info.PayrollDB)))
                {
                    return CustomMessageHandler.Error("Payroll Database doesn't exist!");
                }
                else if (!(await repo.CheckDBIfExists(info.TKSDB)))
                {
                    return CustomMessageHandler.Error("Timekeeping Database doesn't exist!");
                }
                else if (!(await repo.CheckDBIfExists(info.HRISDB)))
                {
                    return CustomMessageHandler.Error("HRIS Database doesn't exist!");
                }
                else
                {
                    var compDto = mapper.Map<CompanyInfoDto>(info);
                    await repo.Insert(compDto);

                    return CustomMessageHandler.RecordAdded();
                }
            }else
            {
                // Update
                if (!(await repo.CheckDBIfExists(info.PayrollDB)))
                {
                    return CustomMessageHandler.Error("Payroll Database doesn't exist!");
                }
                else if (!(await repo.CheckDBIfExists(info.TKSDB)))
                {
                    return CustomMessageHandler.Error("Timekeeping Database doesn't exist!");
                }
                else if (!(await repo.CheckDBIfExists(info.HRISDB)))
                {
                    return CustomMessageHandler.Error("HRIS Database doesn't exist!");
                }
                else
                {
                    //info.LogoForReports = ConvertFileToByte(info.LogoForReportsFile);
                    //info.LogoForSite = ConvertFileToByte(info.LogoForSiteFile);
                    //info.ContentForSite = ConvertFileToByte(info.ContentForSiteFile);

                    var compDto = mapper.Map<CompanyInfoDto>(info);
                    await repo.Update(compDto);

                    return CustomMessageHandler.RecordUpdated();
                }
            }

            
        }

        public string CompanyCode => httpContextAccessor.HttpContext.User.FindFirst("CompanyCode")?.Value;
    }
}
