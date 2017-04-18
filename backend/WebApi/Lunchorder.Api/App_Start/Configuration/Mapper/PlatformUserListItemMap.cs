using AutoMapper;
using Lunchorder.Domain.Entities.DocumentDb;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class PlatformUserListItemMap : Profile
    {
        public PlatformUserListItemMap()
        {
            CreateMap<PlatformUserListItem, Domain.Dtos.PlatformUserListItem>();
        }
    }
}