using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Data;
using WebAPI.Helpers;
using WebAPI.Repositories;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;
using WebAPI.Services;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
            Configuration = builder.Build();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAutoMapper();

            services.AddCors();

            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Repositories
            services.AddScoped<ISampleRepository, SampleRepository>();
            services.AddScoped<ICompanyInformationRepository, CompanyInformationRepository>();
            services.AddScoped<ICitizenshipRepository, CitizenshipRepository>();
            services.AddScoped<IReligionRepository, ReligionRepository>();
            services.AddScoped<IEmployeeStatusFileRepository, EmployeeStatusFileRepository>();
            services.AddScoped<IJobLevelRepository, JobLevelRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<IStepRepository, StepRepository>();
            services.AddScoped<IDesignationFileRepository, DesignationFileRepository>();
            services.AddScoped<IAuditTrailRepository, AuditTrailRepository>();
            services.AddScoped<IAffiliationsRepository, AffiliationsRepository>();
            services.AddScoped<IPersonnelRequestTypeRepository, PersonnelRequestTypeRepository>();

            services.AddScoped<IAreaRepository, AreaRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDivisionRepository, DivisionRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<IRankRepository, RankRepository>();
            services.AddScoped<IProjectCodeRepository, ProjectCodeRepository>();
            services.AddScoped<IPayHouseRepository, PayHouseRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<ILevelsOfEmployeeRepository, LevelsOfEmployeeRepository>();
            services.AddScoped<ICourseDegreeRepository, CourseDegreeRepository>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<IDutiesAndResponsibilitiesRepository, DutiesAndResponsibilitiesRepository>();
            services.AddScoped<IFieldOfInterestRepository, FieldOfInterestRepository>();
            services.AddScoped<IGovExamsRepository, GovExamsRepository>();
            services.AddScoped<IJobReqRepository, JobReqRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<ILicenseRepository, LicenseRepository>();
            services.AddScoped<IResidenceTypeRepository, ResidenceTypeRepository>();
            services.AddScoped<ISkillsRepository, SkillsRepository>();
            services.AddScoped<IDocSubmittedRepository, DocSubmittedRepository>();
            services.AddScoped<IMajorRepository, MajorRepository>();
            services.AddScoped<IScreenTypeRepository, ScreenTypeRepository>();
            services.AddScoped<IRatingsRepository, RatingsRepository>();
            services.AddScoped<IOverallRatingsRepository, OverallRatingsRepository>();
            services.AddScoped<IPreEmpReqRepository, PreEmpReqRepository>();
            services.AddScoped<IAppEntryGenInfoRepository, AppEntryGenInfoRepository>();
            services.AddScoped<IAppEntryTextCertRepository, AppEntryTextCertRepository>();
            services.AddScoped<IAppEntryEssayInfoRepository, AppEntryEssayInfoRepository>();

            #region JWT Service Configuration
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetById(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            #endregion


            // Services 
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICompanyInfoService, CompanyInfoService>();
            services.AddScoped(typeof(IAuditTrailService<>), typeof(AuditTrailService<>));
            services.AddScoped<ICitizenshipService, CitizenshipService>();
            services.AddScoped<IReligionService, ReligionService>();
            services.AddScoped<IEmployeeStatusFileService, EmployeeStatusFileService>();
            services.AddScoped<IJobLevelService, JobLevelService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<IStepService, StepService>();
            services.AddScoped<IDesignationFileService, DesignationFileService>();
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDivisionService, DivisionService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<IRankService, RankService>();
            services.AddScoped<IProjectCodeService, ProjectCodeService>();
            services.AddScoped<IPayHouseService, PayHouseService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<ILevelsOfEmployeeService, LevelsOfEmployeeService>();
            services.AddScoped<ICourseDegreeService, CourseDegreeService>();
            services.AddScoped<IMajorService, MajorService>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<IDutiesAndResponsibilitiesService, DutiesAndResponsibilitiesService>();
            services.AddScoped<IFieldOfInterestService, FieldOfInterestService>();
            services.AddScoped<IGovExamsService, GovExamsService>();
            services.AddScoped<IJobReqService, JobReqService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ILicenseService, LicenseService>();
            services.AddScoped<IResidenceTypeService, ResidenceTypeService>();
            services.AddScoped<ISkillsService, SkillsService>();
            services.AddScoped<IDocSubmittedService, DocSubmittedService>();
            services.AddScoped<IAffiliationsService, AffiliationsService>();
            services.AddScoped<IPersonnelRequestTypeService, PersonnelRequestTypeService>();
            services.AddScoped<IScreenTypeService, ScreenTypeService>();
            services.AddScoped<IRatingsService, RatingsService>();
            services.AddScoped<IOverallRatingsService, OverallRatingsService>();
            services.AddScoped<IPreEmpReqService, PreEmpReqService>();
            services.AddScoped<IAppEntryGenInfoService, AppEntryGenInfoService>();
            services.AddScoped<IAppEntryTextCertService, AppEntryTextCertService>();
            services.AddScoped<IAppEntryEssayInfoService, AppEntryEssayInfoService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

        }
    }
}
