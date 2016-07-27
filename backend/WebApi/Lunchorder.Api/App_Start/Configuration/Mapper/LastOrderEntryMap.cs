using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class LastOrderEntryMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.LastOrderEntry, Domain.Dtos.LastOrderEntry>();
        }
    }
}