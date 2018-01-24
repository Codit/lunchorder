using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class BadgeMap : Profile
    {
        public BadgeMap()
        {
            CreateMap<Domain.Dtos.Badge, Domain.Entities.DocumentDb.UserBadge>()
                .ForMember(dest => dest.TimesEarned, src => src.Ignore());
        }
    }
}