using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Lunchorder.Common.Interfaces;
using Moq;

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
            MockedAccountControllerService = new Mock<IAccountControllerService>();
            MockedBalanceControllerService = new Mock<IBalanceControllerService>();
            MockedChecklistControllerService = new Mock<IChecklistControllerService>();
            MockedEmailControllerService = new Mock<IEmailControllerService>();
            MockedFavoriteControllerService = new Mock<IFavoriteControllerService>();
            MockedMenuControllerService = new Mock<IMenuControllerService>();
            MockedOrderControllerService = new Mock<IOrderControllerService>();

            container.Register(Component.For<IAccountControllerService>().Instance(MockedAccountControllerService.Object).LifestylePerWebRequest());
            container.Register(Component.For<IBalanceControllerService>().Instance(MockedBalanceControllerService.Object).LifestylePerWebRequest());
            container.Register(Component.For<IChecklistControllerService>().Instance(MockedChecklistControllerService.Object).LifestylePerWebRequest());
            container.Register(Component.For<IEmailControllerService>().Instance(MockedEmailControllerService.Object).LifestylePerWebRequest());
            container.Register(Component.For<IFavoriteControllerService>().Instance(MockedFavoriteControllerService.Object).LifestylePerWebRequest());
            container.Register(Component.For<IMenuControllerService>().Instance(MockedMenuControllerService.Object).LifestylePerWebRequest());
            container.Register(Component.For<IOrderControllerService>().Instance(MockedOrderControllerService.Object).LifestylePerWebRequest());

        }

        public Mock<IOrderControllerService> MockedOrderControllerService { get; set; }

        public Mock<IMenuControllerService> MockedMenuControllerService { get; set; }

        public Mock<IFavoriteControllerService> MockedFavoriteControllerService { get; set; }

        public Mock<IEmailControllerService> MockedEmailControllerService { get; set; }

        public Mock<IChecklistControllerService> MockedChecklistControllerService { get; set; }

        public Mock<IBalanceControllerService> MockedBalanceControllerService { get; set; }

        public Mock<IAccountControllerService> MockedAccountControllerService { get; set; }
    }
}
