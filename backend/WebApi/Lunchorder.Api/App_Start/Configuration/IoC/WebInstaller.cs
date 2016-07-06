using System.Web.Http;
using System.Web.Http.Dispatcher;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Lunchorder.Common.Interfaces;
using Microsoft.Owin.Security.DataProtection;

namespace Lunchorder.Api.Configuration.IoC
{
    public class WebInstaller : IWindsorInstaller
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly HttpConfiguration _httpconfiguration;

        public WebInstaller(IDataProtectionProvider dataProtectionProvider, HttpConfiguration httpconfiguration)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _httpconfiguration = httpconfiguration;
        }


        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Install(new ApiControllerInstaller());
            container.Install(new ConfigurationInstaller());
            container.Install(new OAuthInstaller(_dataProtectionProvider));

            foreach (var i in container.ResolveAll<IRequiresInitialization>())
            {
                i.Initialize();
            }

            _httpconfiguration.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container));
        }
    }
}