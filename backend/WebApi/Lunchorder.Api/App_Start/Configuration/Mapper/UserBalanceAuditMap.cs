using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class UserBalanceAuditMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.UserBalanceAudit, Domain.Dtos.UserBalanceAudit>()
                .ForMember(dest => dest.Balance, src => src.Ignore());
        }
    }
}