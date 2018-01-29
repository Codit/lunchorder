using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class BadgeRankingMap : Profile
    {
        public BadgeRankingMap()
        {
            CreateMap<Domain.Entities.DocumentDb.BadgeRanking, Domain.Dtos.BadgeRanking>();
        }
    }
}