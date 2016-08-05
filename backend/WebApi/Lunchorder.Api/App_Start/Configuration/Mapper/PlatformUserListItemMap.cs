using AutoMapper;
using Lunchorder.Domain.Entities.DocumentDb;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class PlatformUserListItemMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<PlatformUserListItem, Domain.Dtos.PlatformUserListItem>();
        }
    }
}