using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Lunchorder.Common.Interfaces;
using Lunchorder.Dal;

namespace Lunchorder.Api.Configuration.IoC
{
    public class DalInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param><param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDocumentStore>()
                    .ImplementedBy<DocumentStore>()
                    .LifestylePerWebRequest());
        }
    }
}