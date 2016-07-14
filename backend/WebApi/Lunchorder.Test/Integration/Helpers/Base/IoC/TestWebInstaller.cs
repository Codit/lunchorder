using System.Web.Http;
using System.Web.Http.Dispatcher;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Lunchorder.Api.Configuration.IoC;
using Lunchorder.Common.Interfaces;
using Microsoft.Owin.Security.DataProtection;

namespace Lunchorder.Test.Integration.Helpers.Base.IoC
{
    public class TestWebInstaller : IWindsorInstaller
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly HttpConfiguration _httpconfiguration;

        public TestWebInstaller(IDataProtectionProvider dataProtectionProvider, HttpConfiguration httpconfiguration)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _httpconfiguration = httpconfiguration;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container
                .Install(new ConfigurationInstaller());

            foreach (var i in container.ResolveAll<IRequiresInitialization>())
            {
                i.Initialize();
            }

            _httpconfiguration.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container));
        }
    }
}
