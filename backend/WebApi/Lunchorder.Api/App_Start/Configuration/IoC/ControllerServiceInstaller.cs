using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Lunchorder.Common.ControllerServices;
using Lunchorder.Common.Interfaces;

namespace Lunchorder.Api.Configuration.IoC
{
    /// <summary>
    /// Windsor installer that registeres <see cref="T:Lunchorder.Common.ControllerServices"/> dependencies
    /// </summary>
    public class ControllerServiceInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param><param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IAccountControllerService>().ImplementedBy<AccountControllerService>());
            container.Register(Component.For<IBalanceControllerService>().ImplementedBy<BalanceControllerService>());
            container.Register(Component.For<IEmailControllerService>().ImplementedBy<EmailControllerService>());
            container.Register(Component.For<IFavoriteControllerService>().ImplementedBy<FavoriteControllerService>());
            container.Register(Component.For<IMenuControllerService>().ImplementedBy<MenuControllerService>());
            container.Register(Component.For<IOrderControllerService>().ImplementedBy<OrderControllerService>());
            container.Register(Component.For<IChecklistControllerService>().ImplementedBy<ChecklistControllerService>());
        }
    }
}