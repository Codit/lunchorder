using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class MenuVendorMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.MenuVendor, Domain.Dtos.MenuVendor>();
        }
    }
}