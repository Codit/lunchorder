using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class MenuVendorMap : Profile
    {
        public MenuVendorMap()
        {
            CreateMap<Domain.Entities.DocumentDb.MenuVendor, Domain.Dtos.MenuVendor>();
            CreateMap<Domain.Dtos.MenuVendor, Domain.Entities.DocumentDb.MenuVendor>();
        }
    }
}