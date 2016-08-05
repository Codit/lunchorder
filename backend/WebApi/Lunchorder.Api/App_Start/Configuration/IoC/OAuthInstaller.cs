using System;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DocumentDB.AspNet.Identity;
using Lunchorder.Api.Infrastructure;
using Lunchorder.Api.Infrastructure.Providers;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Entities.Authentication;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.ActiveDirectory;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;

namespace Lunchorder.Api.Configuration.IoC
{
    public class OAuthInstaller : IWindsorInstaller
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;

        public OAuthInstaller(IDataProtectionProvider dataProtectionProvider)
        {
            if (dataProtectionProvider == null)
                dataProtectionProvider = new DpapiDataProtectionProvider();

            _dataProtectionProvider = dataProtectionProvider;
        }

        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param><param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IdentityFactoryOptions<ApplicationUserManager>>()
                .UsingFactoryMethod(x => new IdentityFactoryOptions<ApplicationUserManager>
                {
                    DataProtectionProvider = _dataProtectionProvider
                }));

            container.Register(Component.For<OAuthAuthorizationServerOptions>()
                .UsingFactoryMethod(k => CreateAuthorizationServerOptions(container))
                .LifestyleSingleton());

            container.Register(Component.For<JwtBearerAuthenticationOptions>()
                .UsingFactoryMethod(CreateJwtBearerAuthenticationOptions)
                .LifestyleSingleton());

            container.Register(Component.For<WindowsAzureActiveDirectoryBearerAuthenticationOptions>()
                    .UsingFactoryMethod(CreateAzureAdOptions)
                    .LifestyleSingleton());

            container.Register(Component.For<IOAuthAuthorizationServerProvider>()
                .ImplementedBy<CustomOAuthProvider>().LifestylePerWebRequest());

            container.Register(Component.For<Func<UserManager<ApplicationUser>>>().Instance(() => container.Resolve<UserManager<ApplicationUser>>())
                .LifestylePerWebRequest());

            container.Register(Component.For<UserManager<ApplicationUser>>()
                .ImplementedBy<ApplicationUserManager>()
                .UsingFactoryMethod(SetupApplicationManager)
                .LifestylePerWebRequest());
        }

        private WindowsAzureActiveDirectoryBearerAuthenticationOptions CreateAzureAdOptions(IKernel container)
        {
            var configurationService = container.Resolve<IConfigurationService>();

            return new WindowsAzureActiveDirectoryBearerAuthenticationOptions
            {
                Tenant = configurationService.AzureAuthentication.Tenant,

                TokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidAudience = configurationService.AzureAuthentication.AudienceId
                }
            };
        }

        private JwtBearerAuthenticationOptions CreateJwtBearerAuthenticationOptions(IKernel container)
        {
            var configurationService = container.Resolve<IConfigurationService>();
            var issuer = configurationService.LocalAuthentication.Issuer;
            string audienceId = configurationService.LocalAuthentication.AudienceId;
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(configurationService.LocalAuthentication.AudienceSecret);

            return new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[]
                {
                    audienceId
                },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                }
            };
        }

        private ApplicationUserManager SetupApplicationManager(IKernel container)
        {
            var configurationService = container.Resolve<IConfigurationService>();
            var docDb = configurationService.DocumentDbAuth;
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new Uri(docDb.Endpoint), docDb.AuthKey,
                docDb.Database, docDb.Collection));

            var dataProtectionProvider = container.Resolve<IdentityFactoryOptions<ApplicationUserManager>>();

            if (dataProtectionProvider != null)
            {
                userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.DataProtectionProvider.Create("ASP.NET Identity"));
            }

            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            userManager.UserLockoutEnabledByDefault = true;
            userManager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            userManager.MaxFailedAccessAttemptsBeforeLockout = 8;

            return userManager;
        }

        private OAuthAuthorizationServerOptions CreateAuthorizationServerOptions(IWindsorContainer container)
        {
            var configurationService = container.Resolve<IConfigurationService>();
            var provider = container.Resolve<IOAuthAuthorizationServerProvider>();

            var options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = configurationService.LocalAuthentication.AllowInsecureHttps,
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = provider,
                AccessTokenFormat = new CustomJwtFormat(configurationService)
            };

            return options;
        }
    }
}