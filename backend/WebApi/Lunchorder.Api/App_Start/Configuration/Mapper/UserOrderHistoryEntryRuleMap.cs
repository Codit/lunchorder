using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class UserOrderHistoryEntryRuleMap : Profile
    {
        public UserOrderHistoryEntryRuleMap()
        {
            CreateMap<Domain.Entities.DocumentDb.UserOrderHistoryEntryRule, Domain.Entities.DocumentDb.VendorHistoryEntryRule>();
        }
    }
}