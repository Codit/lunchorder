using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class MenuVendorClosingDateMap : Profile
    {
        public MenuVendorClosingDateMap()
        {
            CreateMap<Domain.Entities.DocumentDb.MenuVendorClosingDateRange, Domain.Dtos.MenuVendorClosingDateRange>();
            CreateMap<Domain.Dtos.MenuVendorClosingDateRange, Domain.Entities.DocumentDb.MenuVendorClosingDateRange>();
        }
    }
}