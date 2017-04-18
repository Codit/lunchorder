using System;
using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class MenuOrderMap : Profile
    {
        public MenuOrderMap()
        {
            CreateMap<Domain.Dtos.MenuOrder, Domain.Dtos.UserOrderHistoryEntry>()
                .ForMember(dest => dest.Id, source => source.UseValue(Guid.NewGuid()))
                .ForMember(dest => dest.Rules, source => source.MapFrom(x => x.AppliedMenuRules));
        }
    }
}