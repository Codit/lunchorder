using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class UserOrderHistoryMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.UserOrderHistory, Domain.Dtos.UserOrderHistory>();
        }
    }
}