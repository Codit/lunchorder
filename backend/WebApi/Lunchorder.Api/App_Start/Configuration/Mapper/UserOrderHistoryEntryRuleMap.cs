using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class UserOrderHistoryEntryRuleMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.UserOrderHistoryEntryRule, Domain.Entities.DocumentDb.VendorHistoryEntryRule>();
        }
    }
}