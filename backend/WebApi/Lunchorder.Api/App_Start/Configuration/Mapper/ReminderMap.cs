using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class ReminderMap : Profile
    {
        protected override void Configure()
        {
            CreateMap<Domain.Entities.DocumentDb.Reminder, Domain.Dtos.Reminder>();
            CreateMap<Domain.Dtos.Reminder, Domain.Entities.DocumentDb.Reminder>()
                .ForMember(dest => dest.Action, src => src.Ignore());
        }
    }
}