using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class VendorOrderHistoryEntryMap : Profile
    {
        public VendorOrderHistoryEntryMap()
        {
            CreateMap<Domain.Entities.DocumentDb.VendorOrderHistoryEntry, Domain.Dtos.VendorOrderHistoryEntry>();
        }
    }
}