using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class VendorOrderHistoryEntryMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.VendorOrderHistoryEntry, Domain.Dtos.VendorOrderHistoryEntry>();
        }
    }
}