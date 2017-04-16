using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentValidation;
using Lunchorder.Domain.Validators;

namespace Lunchorder.Api.Configuration.IoC
{
    public class ValidationInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param><param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssembly(typeof(PutBalanceRequestValidator).Assembly)
                .BasedOn(typeof(AbstractValidator<>))
                .WithServiceFirstInterface().LifestylePerWebRequest());
        }
    }
}