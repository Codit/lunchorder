using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class MenuRuleMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.MenuRule, Domain.Dtos.MenuRule>();
            CreateMap<Domain.Dtos.MenuRule, Domain.Entities.DocumentDb.MenuRule>();
            CreateMap<Domain.Dtos.MenuRule, Domain.Dtos.UserOrderHistoryRule>();
        }
    }
}