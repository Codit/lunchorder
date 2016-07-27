using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class MenuVendorAddressMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.MenuVendorAddress, Domain.Dtos.MenuVendorAddress>();
            CreateMap<Domain.Dtos.MenuVendorAddress, Domain.Entities.DocumentDb.MenuVendorAddress>();
        }
    }
}