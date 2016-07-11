using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class UserOrderHistoryEntryMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.UserOrderHistoryEntry, Domain.Dtos.UserOrderHistoryEntry>();
        }
    }
}