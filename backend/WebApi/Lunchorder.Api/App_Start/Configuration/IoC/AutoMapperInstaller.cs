using System.Reflection;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Lunchorder.Api.Configuration.Mapper;

namespace Lunchorder.Api.Configuration.IoC
{
    /// <summary>
    /// Windsor installer that registeres <see cref="T:AutoMapper"/> dependencies
    /// </summary>
    public class AutoMapperInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // register value resolvers
            container.Register(Types.FromAssembly(Assembly.GetExecutingAssembly()).BasedOn(typeof(IValueResolver<,,>)));

            // register profiles
            container.Register(Types.FromThisAssembly().BasedOn<Profile>().WithServiceBase().Configure(c => c.Named(c.Implementation.FullName)).LifestyleTransient());

            var profiles = container.ResolveAll<Profile>();

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            container.Register(Component.For<MapperConfiguration>()
                .UsingFactoryMethod(() => config));

            container.Register(
                Component.For<IMapper>().UsingFactoryMethod(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper(ctx.Resolve)));

            var mapper = container.Resolve<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}