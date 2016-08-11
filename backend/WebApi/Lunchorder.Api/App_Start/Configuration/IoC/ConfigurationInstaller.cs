using System.Configuration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Lunchorder.Api.Infrastructure.Services;
using Lunchorder.Common.Interfaces;

namespace Lunchorder.Api.Configuration.IoC
{
    /// <summary>
    /// Windsor installer that registeres <see cref="T:Lunchorder.Api.Controllers"/> dependencies
    /// </summary>
    public class ConfigurationInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param><param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IConfigurationService>()
                    .UsingFactoryMethod(() =>
                        (ConfigurationService) ConfigurationManager.GetSection("lunchorder"))
                    .LifeStyle.HybridPerWebRequestTransient());
        }
    }
}