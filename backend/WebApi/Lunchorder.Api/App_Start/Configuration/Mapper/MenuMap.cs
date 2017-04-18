using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class MenuMap : Profile
    {
        public MenuMap()
        {
            CreateMap<Domain.Entities.DocumentDb.Menu, Domain.Dtos.Menu>();
            CreateMap<Domain.Dtos.Menu, Domain.Entities.DocumentDb.Menu>()
                .ForMember(x => x.Type, dest => dest.Ignore());
        }
    }
}