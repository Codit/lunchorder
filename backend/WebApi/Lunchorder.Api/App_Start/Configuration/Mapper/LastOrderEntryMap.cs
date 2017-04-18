using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class LastOrderEntryMap : Profile
    {
        public LastOrderEntryMap()
        {
            CreateMap<Domain.Entities.DocumentDb.LastOrderEntry, Domain.Dtos.LastOrderEntry>();
        }
    }
}