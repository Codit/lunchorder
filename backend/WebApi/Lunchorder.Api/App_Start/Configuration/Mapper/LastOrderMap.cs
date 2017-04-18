using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class LastOrderMap : Profile
    {
        public LastOrderMap()
        {
            CreateMap<Domain.Entities.DocumentDb.LastOrder, Domain.Dtos.LastOrder>();
        }
    }
}