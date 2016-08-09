using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Lunchorder.Api.Infrastructure.Services;
using Lunchorder.Common.Interfaces;

namespace Lunchorder.Api.Configuration.IoC
{
    public class EventingInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param><param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IEventingService>().ImplementedBy<EventingService>().LifestylePerWebRequest());

            IConfigurationService configurationService = container.Resolve<IConfigurationService>();

            if (configurationService.Servicebus.Enabled)
            {
                container.Register(Component.For<IMessagingService>().ImplementedBy<ServicebusMessagingService>().LifestylePerWebRequest());
            }
        }
    }
}