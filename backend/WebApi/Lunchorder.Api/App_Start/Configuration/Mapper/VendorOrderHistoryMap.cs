using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class VendorOrderHistoryMap : Profile
    {
        public VendorOrderHistoryMap()
        {
            CreateMap<Domain.Entities.DocumentDb.VendorOrderHistory, Domain.Dtos.VendorOrderHistory>();
        }
    }
}