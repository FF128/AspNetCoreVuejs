using AutoMapper;
using WebAPI.Dtos;
using WebAPI.Models;

namespace WebAPI.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CompanyInfoDto, CompanyInformation>();
        }
    }
}
