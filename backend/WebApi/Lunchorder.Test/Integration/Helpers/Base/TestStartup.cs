using Castle.Windsor;
using Lunchorder.Api.Configuration.IoC;
using Lunchorder.Api.Infrastructure.Filters;
using Lunchorder.Common.Interfaces;
using Lunchorder.Test.Integration.Helpers.Base.IoC;
using Owin;
using Startup = Lunchorder.Api.Startup;

namespace Lunchorder.Test.Integration.Helpers.Base
{
    public class TestStartup
    {
        public MockedApiControllerInstaller MockedApiInstaller;

        public void Configuration(IAppBuilder app)
        {
            var startup = new Startup();
            MockedApiInstaller = new MockedApiControllerInstaller();

            Container = new WindsorContainer();

            Container.Kernel.ComponentModelBuilder.AddContributor(new SingletonEqualizer());
            Container
                .Install(MockedApiInstaller)
                .Install(new ApiControllerInstaller())
                .Install(new OAuthInstaller(null))
                .Install(new AutoMapperInstaller())
                .Install(new DalInstaller())
                .Install(new ServiceInstaller())
                .Install(new TestWebInstaller(null, startup.HttpConfiguration));

            var configurationService = Container.Resolve<IConfigurationService>();
            startup.HttpConfiguration.Filters.Add(new ApiKeyActionFilter(configurationService));

            startup.InjectTestContainer(() => Container);
            startup.Configuration(app);
        }

        public WindsorContainer Container { get; set; }
    }
}
