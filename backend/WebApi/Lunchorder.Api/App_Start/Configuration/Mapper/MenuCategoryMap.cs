using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class MenuCategoryMap : Profile
    {
        public MenuCategoryMap()
        {
            CreateMap<Domain.Entities.DocumentDb.MenuCategory, Domain.Dtos.MenuCategory>();
            CreateMap<Domain.Dtos.MenuCategory, Domain.Entities.DocumentDb.MenuCategory>();
        }
    }
}