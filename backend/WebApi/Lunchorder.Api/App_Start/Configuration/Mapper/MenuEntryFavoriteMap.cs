using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class MenuEntryFavoriteMap : Profile
    {
        public MenuEntryFavoriteMap()
        {
            CreateMap<Domain.Entities.DocumentDb.MenuEntryFavorite, Domain.Dtos.MenuEntryFavorite>();
        }
    }
}