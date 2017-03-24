using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Owin;
using PayrollApp.Core.Data.Entities;
using PayrollApp.Rest;
using PayrollApp.Rest.Logger;
using PayrollApp.Rest.Providers;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

[assembly: OwinStartup(typeof(Startup))]
namespace PayrollApp.Rest
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {

            HttpConfiguration config = new HttpConfiguration();

            IUnityContainer _container = UnityConfig.GetConfiguredContainer(config);

            ConfigureOAuth(app, _container);
            ConfigureMapper();

            WebApiConfig.Register(config);

            config.Services.Replace(typeof(IExceptionLogger), _container.Resolve<UnhandledExceptionLogger>());

            var policy = new CorsPolicy()
            {
                AllowAnyHeader = true,
                AllowAnyMethod = true,
                SupportsCredentials = true
            };

            policy.Origins.Add("http://localhost:61072");
            policy.Origins.Add("http://payroll.nullplex.com");
            policy.Origins.Add("http://rsvpweb.ca");
            policy.Origins.Add("http://www.rsvpweb.ca");

            app.UseCors(new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(policy)
                }
            });

            app.MapSignalR();

            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app, IUnityContainer _container)
        {
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = _container.Resolve<OAuthBearerAuthenticationOptions>();

            OAuthAuthorizationServerOptions oAuthServerOptions = _container.Resolve<OAuthAuthorizationServerOptions>();
            oAuthServerOptions.AllowInsecureHttp = true;
            oAuthServerOptions.TokenEndpointPath = new PathString("/token");
            oAuthServerOptions.AccessTokenExpireTimeSpan = TimeSpan.FromDays(1);
            oAuthServerOptions.Provider = _container.Resolve<AuthorizationServerProvider>();

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }

        public void ConfigureMapper()
        {
            Mapper.CreateMap<ReportParameter, ReportRequestParameter>();
            Mapper.CreateMap<Report, ReportRequest>()
                .ForMember(dest => dest.ReportRequestParameters, options => options.MapFrom(source => source.ReportParameters));
        }
    }

    public class TraceDisposable : IDisposable
    {
        public void Dispose()
        {
            Debug.WriteLine("Disposed");
        }
    }
}