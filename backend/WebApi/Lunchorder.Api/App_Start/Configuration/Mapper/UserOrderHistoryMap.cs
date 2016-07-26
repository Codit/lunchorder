using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class UserOrderHistoryMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.UserOrderHistory, Domain.Dtos.UserOrderHistory>();
            CreateMap<Domain.Dtos.UserOrderHistory, Domain.Entities.DocumentDb.UserOrderHistory>()
                .ForMember(x => x.Type, dest => dest.Ignore())
                .ForMember(x => x.UserId, dest => dest.Ignore());
        }
    }
}