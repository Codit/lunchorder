using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class UserOrderHistoryRuleMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.UserOrderHistoryEntryRule, Domain.Dtos.UserOrderHistoryRule>();
            CreateMap<Domain.Dtos.UserOrderHistoryRule, Domain.Entities.DocumentDb.UserOrderHistoryEntryRule>();
        }
    }
}