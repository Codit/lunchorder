using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class MenuEntryMap : Profile
    {
        public MenuEntryMap()
        {
            CreateMap<Domain.Entities.DocumentDb.MenuEntry, Domain.Dtos.MenuEntry>();
            CreateMap<Domain.Dtos.MenuEntry, Domain.Entities.DocumentDb.MenuEntry>();
        }
    }
}