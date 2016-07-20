using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class MenuEntryFavoriteMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.MenuEntryFavorite, Domain.Dtos.MenuEntryFavorite>();
        }
    }
}