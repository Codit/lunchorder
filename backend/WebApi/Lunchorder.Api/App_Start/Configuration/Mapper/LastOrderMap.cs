using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class LastOrderMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.LastOrder, Domain.Dtos.LastOrder>();
        }
    }
}