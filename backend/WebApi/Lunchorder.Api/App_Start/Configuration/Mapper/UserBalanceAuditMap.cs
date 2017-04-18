using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class UserBalanceAuditMap : Profile
    {
        public UserBalanceAuditMap()
        {
            CreateMap<Domain.Entities.DocumentDb.UserBalanceAudit, Domain.Dtos.UserBalanceAudit>()
                .ForMember(dest => dest.Balance, src => src.Ignore());
        }
    }
}