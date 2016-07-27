using System;
using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class UserOrderHistoryMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.UserOrderHistory, Domain.Dtos.UserOrderHistory>();

            CreateMap<Domain.Entities.DocumentDb.UserOrderHistory, Domain.Entities.DocumentDb.LastOrder>()
                .ForMember(dest => dest.UserOrderHistoryId, source => source.Ignore())
                .ForMember(dest => dest.LastOrderEntries, source => source.MapFrom(y => y.Entries));

            CreateMap<Domain.Dtos.UserOrderHistory, Domain.Entities.DocumentDb.UserOrderHistory>()
                .ForMember(dest => dest.Type, source => source.Ignore())
                .ForMember(dest => dest.UserId, source => source.Ignore());
        }
    }
}