using AutoMapper;
using WebAPI.Dtos;
using WebAPI.Dtos.CourseDto;
using WebAPI.Dtos.MajorDto;
using WebAPI.Dtos.PRDto;
using WebAPI.Dtos.School;
using WebAPI.Dtos.SkillsDto;
using WebAPI.Models;
using WebAPI.Models.PRModel;

namespace WebAPI.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CompanyInfoDto, CompanyInformation>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<PreEmpReqEmpStatusDesignationDto, PreEmpReq>();

            CreateMap<DesignationDutiesReqDto, DesignationDuties>();
            // School
            CreateMap<SchoolInsertToHRISFSDto, School>();
            CreateMap<School, SchoolInsertToHRISFSDto>();
            CreateMap<School, SchoolUpdateToHRISFSDto>();
            CreateMap<SchoolUpdateToHRISFSDto, School>();

            // Course
            CreateMap<CourseInsertToHRISFSDto, CourseDegree>();
            CreateMap<CourseDegree, CourseInsertToHRISFSDto>();
            CreateMap<CourseUpdateToHRISFSDto, CourseDegree>();
            CreateMap<CourseDegree, CourseUpdateToHRISFSDto>();
            //Skills
            CreateMap<SkillInsertToHRISFSDto, Skills>();
            CreateMap<Skills, SkillInsertToHRISFSDto>();
            CreateMap<SkillUpdateToHRISFSDto, Skills>();
            CreateMap<Skills, SkillUpdateToHRISFSDto>();
            // Major
            CreateMap<MajorInsertToHRISFSDto, Major>();
            CreateMap<Major, MajorInsertToHRISFSDto>();
            CreateMap<MajorUpdateToHRISFSDto, Major>();
            CreateMap<Major, MajorUpdateToHRISFSDto>();

            // Personnel Requisition
            CreateMap<PRFHeaderMaintDto, PRFHeaderMaint>();
            CreateMap<PRFDetailsMaintAttachmentDto, PRFDetailsMaintAttachment>();
            CreateMap<PRFExtendDto, PRFExtend>();
            CreateMap<PRFMainReturnCommentDto, PRFMainReturnComment>();
            CreateMap<PRFDetailsMaintDto, PRFDetailsMaint>();

        }
    }
}
