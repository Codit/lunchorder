using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class SimpleUserMap : Profile
    {
        public SimpleUserMap()
        {
            CreateMap<Domain.Dtos.SimpleUser, Domain.Entities.DocumentDb.SimpleUser>();

        }
    }
}