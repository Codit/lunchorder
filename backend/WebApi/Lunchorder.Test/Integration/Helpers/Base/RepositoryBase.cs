using Castle.Windsor;
using Lunchorder.Api.Configuration.IoC;
using Lunchorder.Common.Interfaces;
using NUnit.Framework;

namespace Lunchorder.Test.Integration.Helpers.Base
{
    public class RepositoryBase
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
                .Install(new AutoMapperInstaller())
                .Install(new ConfigurationInstaller())
                .Install(new DalInstaller());

            var documentDbBase = new DocumentDbBase(_container.Resolve<IDocumentStore>());
            documentDbBase.Init();

            DatabaseRepository = _container.Resolve<IDatabaseRepository>();
        }

        public IDatabaseRepository DatabaseRepository { get; set; }
    }
}