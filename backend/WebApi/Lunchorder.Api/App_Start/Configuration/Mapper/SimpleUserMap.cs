using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class SimpleUserMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Dtos.SimpleUser, Domain.Entities.DocumentDb.SimpleUser>();

        }
    }
}