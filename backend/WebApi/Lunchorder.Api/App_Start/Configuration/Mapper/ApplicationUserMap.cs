using AutoMapper;
using Lunchorder.Domain.Dtos;
using Lunchorder.Domain.Entities.Authentication;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class ApplicationUserMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<ApplicationUser, Domain.Dtos.Responses.GetUserInfoResponse>()
                .ForMember(x => x.UserToken, y => y.Ignore())
                .ForMember(x => x.Profile, y => y.Ignore())
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