using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class UserBalanceAuditItemMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.UserBalanceAuditItem, Domain.Dtos.UserBalanceAuditItem>()
                .ForMember(x => x.OriginatorName, dest => dest.MapFrom(x => x.Originator.UserName));
        }
    }
}