using AutoMapper;
using Lunchorder.Domain.Entities.DocumentDb;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class PlatformUserListMap : Profile
    {
        public PlatformUserListMap()
        {
            CreateMap<PlatformUserList, Domain.Dtos.PlatformUserList>(); 
        }
    }
}