using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class MenuCategoryMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.MenuCategory, Domain.Dtos.MenuCategory>();
        }
    }
}