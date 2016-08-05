using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class VendorHistoryEntryRuleMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.VendorHistoryEntryRule, Domain.Dtos.VendorHistoryEntryRule>();
        }
    }
}