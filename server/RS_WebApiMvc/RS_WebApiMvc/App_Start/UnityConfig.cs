using RS_WebApiMvc.Data;
using RS_WebApiMvc.Repositories;
using RS_WebApiMvc.RepositoryInterfaces;
using System;

using Unity;

namespace RS_WebApiMvc
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();
            
            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IConnectionFactory, ConnectionFactory>();

            container.RegisterType<ICitizenshipRepository, CitizenshipRepository>();
            container.RegisterType<IAreaRepository, AreaRepository>();
            container.RegisterType<IBranchRepository, BranchRepository>();
            container.RegisterType<IDepartmentRepository, DepartmentRepository>();
            container.RegisterType<IDivisionRepository, DivisionRepository>();
            container.RegisterType<ILocationRepository, LocationRepository>();
            container.RegisterType<IUnitRepository, UnitRepository>();
            container.RegisterType<ISectionRepository, SectionRepository>();
            container.RegisterType<IRankRepository, RankRepository>();
            container.RegisterType<IProjectCodeRepository, ProjectCodeRepository>();
            container.RegisterType<IPayHouseRepository, PayHouseRepository>();
            container.RegisterType<IRegionRepository, RegionRepository>();
            container.RegisterType<ILevelsOfEmployeeRepository, LevelsOfEmployeeRepository>();
            container.RegisterType<ICourseDegreeRepository, CourseDegreeRepository>();
            container.RegisterType<ISchoolRepository, SchoolRepository>();
            container.RegisterType<IDutiesAndResponsibilitiesRepository, DutiesAndResponsibilitiesRepository>();
            container.RegisterType<IFieldOfInterestRepository, FieldOfInterestRepository>();
            container.RegisterType<IGovExamsRepository, GovExamsRepository>();
            container.RegisterType<IJobReqRepository, JobReqRepository>();
            container.RegisterType<ILanguageRepository, LanguageRepository>();
            container.RegisterType<ILicenseRepository, LicenseRepository>();
            container.RegisterType<IResidenceTypeRepository, ResidenceTypeRepository>();
            container.RegisterType<ISkillsRepository, SkillsRepository>();
            container.RegisterType<IDocSubmittedRepository, DocSubmittedRepository>();
        }
    }
}