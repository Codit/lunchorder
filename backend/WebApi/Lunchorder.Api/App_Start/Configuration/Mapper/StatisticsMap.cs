using System.Linq;
using AutoMapper;

namespace Lunchorder.Api.Configuration.Mapper
{
    public class StatisticsMap : Profile
    {
        public StatisticsMap()
        {
            CreateMap<Domain.Entities.DocumentDb.Statistics, Domain.Dtos.Statistics>()
                .ForMember(dest => dest.WeeklyTotalAmount, src => src.ResolveUsing(y =>
                {
                    var weeklyTotal = y.WeeklyTotals.FirstOrDefault();
                    return weeklyTotal?.Amount ?? 0;
                }))
                .ForMember(dest => dest.MonthlyTotalAmount, src => src.ResolveUsing(y =>
                {
                    var monthlyTotal = y.WeeklyTotals.FirstOrDefault();
                    return monthlyTotal?.Amount ?? 0;
                }))
                .ForMember(dest => dest.YearlyTotalAmount, src => src.ResolveUsing(y =>
                {
                    var yearlyTotal = y.YearlyTotals.FirstOrDefault();
                    return yearlyTotal?.Amount ?? 0;
                }));
        }
    }
}