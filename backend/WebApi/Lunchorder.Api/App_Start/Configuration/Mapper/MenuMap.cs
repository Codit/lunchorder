using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class MenuMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.Menu, Domain.Dtos.Menu>();
        }
    }
}