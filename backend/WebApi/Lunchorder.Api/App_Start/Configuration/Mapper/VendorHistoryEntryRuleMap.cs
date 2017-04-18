using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class VendorHistoryEntryRuleMap : Profile
    {
        public VendorHistoryEntryRuleMap()
        {
            CreateMap<Domain.Entities.DocumentDb.VendorHistoryEntryRule, Domain.Dtos.VendorHistoryEntryRule>();
        }
    }
}