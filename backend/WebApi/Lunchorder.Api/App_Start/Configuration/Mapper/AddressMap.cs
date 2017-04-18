using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class AddressMap : Profile
    {
        protected AddressMap()
        {
            CreateMap<Domain.Entities.DocumentDb.Address, Domain.Dtos.Address>();
        }
    }
}