using System;
using Castle.Windsor;
using FluentValidation;

namespace Lunchorder.Api.Configuration.IoC
{
    public class WindsorValidatorFactory : ValidatorFactoryBase
    {
        private readonly IWindsorContainer _container;

        public WindsorValidatorFactory(IWindsorContainer container)
        {
            _container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            if (_container.Kernel.HasComponent(validatorType))
                return _container.Resolve(validatorType) as IValidator;
            return null;
        }
    }
}