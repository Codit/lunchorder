using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Lunchorder.Api.Controllers;

namespace Lunchorder.Test.Integration.Helpers.Base.IoC
{
    /// <summary>
    /// Windsor installer that registeres <see cref="T:Lunchorder.Api.Controllers"/> dependencies
    /// </summary>
    public class MockedApiControllerInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param><param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining(typeof(BaseApiController))
                    .BasedOn<BaseApiController>()
                    .LifestylePerWebRequest()
                    .Configure(x => x.Named(x.Implementation.FullName))
                );
        }
    }
}
