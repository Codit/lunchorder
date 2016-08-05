using AutoMapper;
using Lunchorder.Domain.Entities.DocumentDb;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class PlatformUserListMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<PlatformUserList, Domain.Dtos.PlatformUserList>(); 
        }
    }
}