using System.Linq;
using AutoMapper;
using Lunchorder.Domain.Dtos;
using Lunchorder.Domain.Entities.Authentication;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class ApplicationUserMap : Profile
    {
        public ApplicationUserMap()
        {
            CreateMap<ApplicationUser, Domain.Dtos.Responses.GetUserInfoResponse>()
                .ForMember(x => x.UserToken, y => y.Ignore())
                .ForMember(x => x.Profile, y => y.Ignore())
                .ForMember(x => x.Roles, y => y.MapFrom(z => z.Roles.Select(x => x.RoleName)))
                .AfterMap((src, dest) => dest.Profile = new UserProfile
                {
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    Picture = src.Picture,
                    Culture = src.Culture
                });
        }
    }
}