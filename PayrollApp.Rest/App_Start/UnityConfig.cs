using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using PayrollApp.Repository;
using PayrollApp.Rest.Helpers;
using PayrollApp.Rest.Logger;
using PayrollApp.Rest.Providers;
using PayrollApp.Service.IServices;
using PayrollApp.Service.Services;
using System.Data.Entity;
using System.Web.Http;
using Unity.WebApi;

namespace PayrollApp.Rest
{
    public static class UnityConfig
    {
        public static IUnityContainer GetConfiguredContainer(HttpConfiguration config)
        {
            UnityContainer container;
            RegisterComponents(out container, config);
            return container;
        }

        public static void RegisterComponents(out UnityContainer container, HttpConfiguration config)
        {
            container = new UnityContainer();

            container.RegisterType(typeof(Startup));
            container.RegisterType(typeof(AuthorizationServerProvider));
            container.RegisterType(typeof(OAuthBearerAuthenticationOptions));
            container.RegisterType(typeof(OAuthAuthorizationServerOptions));
            container.RegisterType(typeof(UnhandledExceptionLogger));

            container.RegisterType<DbContext, PayrollAppDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType(typeof(IRepository<>), typeof(GenericRepository<>));
            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<ILastLoginService, LastLoginService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IRoleService, RoleService>();
            container.RegisterType<IUserRoleService, UserRoleService>();
            container.RegisterType<IExcLoggerService, ExcLoggerService>();
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<IEmployeeNoteService, EmployeeNoteService>();
            container.RegisterType<IEmployeeTypeService, EmployeeTypeService>();
            container.RegisterType<ICountryService, CountryService>();
            container.RegisterType<IStateService, StateService>();
            container.RegisterType<ICityService, CityService>();
            container.RegisterType<IPayFrequencyService, PayFrequencyService>();
            container.RegisterType<ILabourClassificationService, LabourClassificationService>();
            container.RegisterType<IEmployeeLabourClassificationService, EmployeeLabourClassificationService>();
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<IEmployeeBlacklistService, EmployeeBlacklistService>();
            container.RegisterType<ICertificationService, CertificationService>();
            container.RegisterType<IEmployeeCertificationService, EmployeeCertificationService>();
            container.RegisterType<ITitleService, TitleService>();
            container.RegisterType<ISkillService, SkillService>();
            container.RegisterType<IEmployeeSkillService, EmployeeSkillService>();
            container.RegisterType<IPaymentTermService, PaymentTermService>();
            container.RegisterType<ICustomerSiteService, CustomerSiteService>();
            container.RegisterType<ICustomerSiteNoteService, CustomerSiteNoteService>();
            container.RegisterType<ISalesRepService, SalesRepService>();
            container.RegisterType<ICustomerSiteLabourClassificationService, CustomerSiteLabourClassificationService>();
            container.RegisterType<IEquipmentService, EquipmentService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IOrderEquipmentService, OrderEquipmentService>();
            container.RegisterType<IOrderTimeslipService, OrderTimeslipService>();
            container.RegisterType<IImageService, ImageService>();
            container.RegisterType<ICustomerSiteJobLocationService, CustomerSiteJobLocationService>();
            container.RegisterType<IReportService, ReportService>();
            container.RegisterType<IPreferenceService, PreferenceService>();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}