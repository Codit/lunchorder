using Castle.Windsor;
using Lunchorder.Api.Configuration.IoC;
using Lunchorder.Api.Infrastructure.Services;
using Lunchorder.Common.Interfaces;
using NUnit.Framework;

namespace Lunchorder.Test.Integration.Helpers.Base
{
    public class RepositoryBase
    {
        private WindsorContainer _container;
        public DocumentDbBase DocumentDbBase;

        /// <summary>
        /// Runs before each tests is launched
        /// </summary>
        [SetUp]
        public virtual void BeforeEachTest()
        {
            _container = new WindsorContainer();

            _container.Kernel.ComponentModelBuilder.AddContributor(new SingletonEqualizer());
            _container
                .Install(new AutoMapperInstaller())
                .Install(new ServiceInstaller())
                .Install(new ConfigurationInstaller())
                .Install(new DalInstaller());

            DocumentDbBase = new DocumentDbBase(_container.Resolve<IDocumentStore>(), _container.Resolve<SeedService>());
            DocumentDbBase.Init();


            DatabaseRepository = _container.Resolve<IDatabaseRepository>();
        }

        public IDatabaseRepository DatabaseRepository { get; set; }
    }

    public class RepositoryBaseSeededDb
    {
        private WindsorContainer _container;
        public DocumentDbBase DocumentDbBase;

        /// <summary>
        /// Runs before each tests is launched
        /// </summary>
        [SetUp]
        public virtual void BeforeEachTest()
        {
            _container = new WindsorContainer();

            _container.Kernel.ComponentModelBuilder.AddContributor(new SingletonEqualizer());
            _container
                .Install(new AutoMapperInstaller())
                .Install(new ServiceInstaller())
                .Install(new ConfigurationInstaller())
                .Install(new DalInstaller());

            DocumentDbBase = new DocumentDbBase(_container.Resolve<IDocumentStore>(), _container.Resolve<SeedService>());

            DatabaseRepository = _container.Resolve<IDatabaseRepository>();
            BadgeService = _container.Resolve<IBadgeService>();
        }

        public IBadgeService BadgeService { get; set; }
        public IDatabaseRepository DatabaseRepository { get; set; }
    }
}