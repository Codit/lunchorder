using AutoMapper;
using Lunchorder.Domain.Entities.DocumentDb;

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