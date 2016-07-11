using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class AddressMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.Address, Domain.Dtos.Address>();
        }
    }
}