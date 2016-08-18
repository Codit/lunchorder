using System;
using System.Linq;
using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class UserOrderHistoryEntryMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.UserOrderHistoryEntry, Domain.Dtos.UserOrderHistoryEntry>();
            CreateMap<Domain.Entities.DocumentDb.UserOrderHistoryEntry, Domain.Entities.DocumentDb.LastOrderEntry>()
                .ForMember(dest => dest.AppliedRules, source => source.MapFrom(y => string.Join("\n", y.Rules.Select(x => x.Description))));

            CreateMap<Domain.Entities.DocumentDb.UserOrderHistoryEntry, Domain.Entities.DocumentDb.VendorOrderHistoryEntry>()
                .ForMember(dest => dest.UserId, source => source.Ignore())
                .ForMember(dest => dest.UserName, source => source.Ignore())
                .ForMember(dest => dest.FullName, source => source.Ignore())
                .ForMember(dest => dest.Id, source => source.UseValue(Guid.NewGuid()))
                .ForMember(dest => dest.UserOrderHistoryEntryId, source => source.MapFrom(y => y.Id));

            CreateMap<Domain.Dtos.UserOrderHistoryEntry, Domain.Entities.DocumentDb.UserOrderHistoryEntry>();
        }
    }
}