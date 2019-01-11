using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Dtos;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Repositories
{
    public class CompanyInformationRepository: ICompanyInformationRepository
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly ICompanyInfoService service;
        private readonly IMapper mapper;
        public CompanyInformationRepository(IConnectionFactory connectionFactory,
            ICompanyInfoService service,
            IMapper mapper)
        {
            this.connectionFactory = connectionFactory;
            this.service = service;
            this.mapper = mapper;
        }

        public async Task Delete(int id)
        {
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_CompanyInfo_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<CompanyInfoDto>> GetAll()
        {
            using(var conn = connectionFactory.Connection)
            {
                
                var companyInfo = await conn.QueryAsync<CompanyInformation>("sp_CompanyInfo_View");

                return mapper.Map<IEnumerable<CompanyInfoDto>>(companyInfo);
            }
        }

        public async Task<CompanyInfoDto> GetByCompanyCode(string code)
        {
            using(var conn = connectionFactory.Connection)
            {
                var companyInfo =
                    await conn.QueryFirstOrDefaultAsync<CompanyInformation>("sp_CompanyInfo_GetByCompanyCode",
                        new { CompanyCode = code },
                        commandType: CommandType.StoredProcedure);
                return mapper.Map<CompanyInfoDto>(companyInfo);
            }
        }

        public async Task<CompanyInfoDto> GetById(int id)
        {
            using(var conn = connectionFactory.Connection)
            {
                var companyInfo =
                    await conn.QueryFirstOrDefaultAsync<CompanyInformation>("sp_CompanyInfo_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);

                return mapper.Map<CompanyInfoDto>(companyInfo);
            }
        }

        public async Task Insert(CompanyInformation info)
        {
            using (var conn = connectionFactory.Connection)
            {
                try
                {
                    info.LogoForReports = service.ConvertFileToByte(info.LogoForReportsFile);
                    info.LogoForSite = service.ConvertFileToByte(info.LogoForSiteFile);
                    info.ContentForSite = service.ConvertFileToByte(info.ContentForSiteFile);

                    var ciDto = mapper.Map<CompanyInfoDto>(info);

                    await conn.ExecuteAsync("sp_CompanyInfo_Insert",
                        ciDto, commandType: CommandType.StoredProcedure);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
        }

        public async Task Update(CompanyInformation info)
        {
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_CompanyInfo_Update",
                    info, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
