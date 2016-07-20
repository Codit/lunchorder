using AutoMapper;
using Lunchorder.Domain.Entities.DocumentDb;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class UserBadgeMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<UserBadge, Domain.Dtos.UserBadge>();
        }
    }
}