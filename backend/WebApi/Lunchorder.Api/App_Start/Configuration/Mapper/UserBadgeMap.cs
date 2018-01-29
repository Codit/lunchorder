using AutoMapper;
using UserBadge = Lunchorder.Domain.Entities.DocumentDb.UserBadge;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class UserBadgeMap : Profile
    {
        public UserBadgeMap()
        {
            CreateMap<UserBadge, Domain.Dtos.UserBadge>();
            CreateMap<Domain.Dtos.UserBadge, UserBadge>();
        }
    }
}