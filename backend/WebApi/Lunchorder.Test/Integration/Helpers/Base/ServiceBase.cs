using Castle.Windsor;
using Lunchorder.Api.Configuration.IoC;
using Lunchorder.Api.Infrastructure.Services;
using Lunchorder.Common.Interfaces;
using NUnit.Framework;

namespace Lunchorder.Test.Integration.Helpers.Base
{
    public class ServiceBase
    {
        private WindsorContainer _container;

        /// <summary>
        /// Runs before each tests is launched
        /// </summary>
        [SetUp]
        public virtual void BeforeEachTest()
        {
            _container = new WindsorContainer();

            _container.Kernel.ComponentModelBuilder.AddContributor(new SingletonEqualizer());
            _container
                .Install(new OAuthInstaller(null))
                .Install(new AutoMapperInstaller())
                .Install(new ConfigurationInstaller())
                .Install(new ServiceInstaller())
                .Install(new DalInstaller());

            var documentDbBase = new DocumentDbBase(_container.Resolve<IDocumentStore>(), _container.Resolve<SeedService>());
            documentDbBase.Init();

            ApplicationUserService = _container.Resolve<IUserService>();
        }

        public IUserService ApplicationUserService { get; set; }
    }
}