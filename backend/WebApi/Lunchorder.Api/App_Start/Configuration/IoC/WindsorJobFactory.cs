using System;
using Castle.Windsor;
using FluentScheduler;

namespace Lunchorder.Api.Configuration.IoC
{
    public class WindsorJobFactory : IJobFactory
    {
        private readonly IWindsorContainer _container;

        public WindsorJobFactory(IWindsorContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            _container = container;
        }

        public IJob GetJobInstance<T>() where T : IJob
        {
            return _container.Resolve<T>();
        }
    }
}