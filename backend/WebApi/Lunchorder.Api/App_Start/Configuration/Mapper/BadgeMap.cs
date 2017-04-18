using AutoMapper;
using Lunchorder.Domain.Entities.DocumentDb;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class BadgeMap : Profile
    {
        public BadgeMap()
        {
            CreateMap<Badge, Domain.Dtos.Badge>();
        }
    }
}