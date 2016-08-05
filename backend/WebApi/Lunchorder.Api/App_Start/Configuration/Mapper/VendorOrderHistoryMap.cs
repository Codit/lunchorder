using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class VendorOrderHistoryMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.VendorOrderHistory, Domain.Dtos.VendorOrderHistory>();
        }
    }
}