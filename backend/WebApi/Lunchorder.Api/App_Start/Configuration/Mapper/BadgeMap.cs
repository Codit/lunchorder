using AutoMapper;
using Lunchorder.Domain.Entities.DocumentDb;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class BadgeMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Badge, Domain.Dtos.Badge>();
        }
    }
}