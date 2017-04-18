using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class UserBalanceAuditItemMap : Profile
    {
        public UserBalanceAuditItemMap()
        {
            CreateMap<Domain.Entities.DocumentDb.UserBalanceAuditItem, Domain.Dtos.UserBalanceAuditItem>()
                .ForMember(x => x.OriginatorName, dest => dest.MapFrom(x => x.Originator.FullName));
        }
    }
}