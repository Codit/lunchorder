using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Castle.Windsor;
using FluentScheduler;
using FluentValidation.WebApi;
using Lunchorder.Api.Configuration.IoC;
using Lunchorder.Api.Infrastructure.Filters;
using Lunchorder.Api.Infrastructure.Services;
using Lunchorder.Common.Extensions;
using Lunchorder.Domain.Dtos;
using Microsoft.Owin;
using Microsoft.Owin.Security.ActiveDirectory;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using Startup = Lunchorder.Api.Startup;

[assembly: OwinStartup(typeof(Startup))]
namespace Lunchorder.Api
{
    public partial class Startup
    {
        private HttpConfiguration _httpConfiguration;
        public HttpConfiguration HttpConfiguration => _httpConfiguration ?? (_httpConfiguration = new HttpConfiguration());

        private IWindsorContainer _container = null;

        public void Configuration(IAppBuilder app)
        {
            if (_container == null)
            {
                _container = new WindsorContainer();
                _container.Install(new WebInstaller(null, HttpConfiguration));

                SwaggerConfig.Register(HttpConfiguration);
                var seedService = _container.Resolve<SeedService>();
                seedService.SeedDocuments().Wait();
                seedService.SeedStoredProcedures().Wait();
            }
            
            var oAuthBearerOptions = _container.Resolve<JwtBearerAuthenticationOptions>();
            var oAuthServerOptions = _container.Resolve<OAuthAuthorizationServerOptions>();
            var azureAdServerOptions = _container.Resolve<WindowsAzureActiveDirectoryBearerAuthenticationOptions>();
            var authorizationOptions = new AuthorizationOptions
            {
                OAuthAuthorizationServerOptions = oAuthServerOptions, JwtBearerAuthenticationOptions = oAuthBearerOptions,
                AzureAdServerOptions = azureAdServerOptions
            };

            JobManager.JobFactory = new WindsorJobFactory(_container);
            JobManager.Initialize(new JobRegistry(_container));

            ConfigureAuth(app, authorizationOptions);
            ConfigureWebApi();


            app.MapSignalR();
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(HttpConfiguration);
        }

        private void ConfigureWebApi()
        {
            HttpConfiguration.MapHttpAttributeRoutes();

            HttpConfiguration.Filters.Add(new ExceptionHandlingFilter());

            FluentValidationModelValidatorProvider.Configure(HttpConfiguration);

            var jsonFormatter = HttpConfiguration.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public void InjectTestContainer(Func<IWindsorContainer> setupContainer)
        {
            _container = setupContainer();
        }
    }
}