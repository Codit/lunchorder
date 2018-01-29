using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos;
using Lunchorder.Domain.Entities.Authentication;
using Lunchorder.Domain.Entities.DocumentDb;
using UserBadge = Lunchorder.Domain.Entities.DocumentDb.UserBadge;

namespace Lunchorder.Api.Infrastructure.Services
{
    public class BadgeService : IBadgeService
    {
        private readonly IMapper _mapper;
        public List<Badge> Badges;

        public BadgeService(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Badges = Domain.Constants.Badges.BadgeList;
        }

        private string OrderBadgeMessage(Badge badge)
        {
            return $"You earned the {badge.Name} badge";
        }

        private string PrepayBadgeMessage(Badge badge, string username)
        {
            return $"{username} earned the {badge.Name} badge";
        }

        private void CalculateStatistics(ApplicationUser applicationUser, Domain.Entities.DocumentDb.UserOrderHistory userOrderHistory)
        {
            if (!userOrderHistory.StatisticsProcessed)
            {
                var monthlyTotal =
                    applicationUser.Statistics.MonthlyTotals.FirstOrDefault(x =>
                        x.MonthDate == x.ParseMonth(userOrderHistory.OrderTime));
                if (monthlyTotal == null)
                {
                    monthlyTotal = new MonthlyTotal(userOrderHistory.OrderTime);
                    applicationUser.Statistics.MonthlyTotals.Add(monthlyTotal);
                }

                var yearlyTotal =
                    applicationUser.Statistics.YearlyTotals.FirstOrDefault(x =>
                        x.YearDate == x.ParseYear(userOrderHistory.OrderTime));
                if (yearlyTotal == null)
                {
                    yearlyTotal = new YearlyTotal(userOrderHistory.OrderTime);
                    applicationUser.Statistics.YearlyTotals.Add(yearlyTotal);
                }

                var weeklyTotal =
                    applicationUser.Statistics.WeeklyTotals.FirstOrDefault(x =>
                        x.WeekDate == x.ParseWeek(userOrderHistory.OrderTime));
                if (weeklyTotal == null)
                {
                    weeklyTotal = new WeeklyTotal(userOrderHistory.OrderTime);
                    applicationUser.Statistics.WeeklyTotals.Add(weeklyTotal);
                }

                var dailyTotal =
                    applicationUser.Statistics.DayTotals.FirstOrDefault(x =>
                        x.DayDate == x.ParseDay(userOrderHistory.OrderTime));
                if (dailyTotal == null)
                {
                    dailyTotal = new DayTotal(userOrderHistory.OrderTime);
                    applicationUser.Statistics.DayTotals.Add(dailyTotal);
                }

                foreach (var entry in userOrderHistory.Entries)
                {
                    if (entry.Pasta) { yearlyTotal.PastaOrderCount += 1; }
                    if (entry.Healthy) { weeklyTotal.HealthyOrderCount += 1; }

                    yearlyTotal.OrderCount += 1;
                    yearlyTotal.Amount += entry.FinalPrice;
                    weeklyTotal.OrderCount += 1;
                    weeklyTotal.Amount += entry.FinalPrice;
                    monthlyTotal.OrderCount += 1;
                    monthlyTotal.Amount += entry.FinalPrice;
                    dailyTotal.Amount += entry.FinalPrice;
                    dailyTotal.OrderCount += 1;
                }

                applicationUser.Statistics.AppTotalSpend += userOrderHistory.FinalPrice;
                userOrderHistory.StatisticsProcessed = true;

                // cleanup old data
                var daysToDelete = applicationUser.Statistics.DayTotals.Where(x => x.DayDate != x.ParseDay(DateTime.UtcNow)).ToList();
                foreach (var dayToDelete in daysToDelete) { applicationUser.Statistics.DayTotals.Remove(dayToDelete); }

                var weeksToDelete = applicationUser.Statistics.WeeklyTotals.Where(x => x.WeekDate != x.ParseWeek(DateTime.UtcNow)).ToList();
                foreach (var weekToDelete in weeksToDelete) { applicationUser.Statistics.WeeklyTotals.Remove(weekToDelete); }

                var monthsToDelete = applicationUser.Statistics.MonthlyTotals.Where(x => x.MonthDate != x.ParseMonth(DateTime.UtcNow)).ToList();
                foreach (var monthToDelete in monthsToDelete) { applicationUser.Statistics.MonthlyTotals.Remove(monthToDelete); }

                var yearsToDelete = applicationUser.Statistics.YearlyTotals.Where(x => x.YearDate != x.ParseYear(DateTime.UtcNow)).ToList();
                foreach (var yearToDelete in yearsToDelete) { applicationUser.Statistics.YearlyTotals.Remove(yearToDelete); }
            }
        }

        public List<string> ExtractPrepayBadges(ApplicationUser applicationUser, decimal prepayAmount)
        {
            var badgeAlerts = new List<string>();
            if (HaveFaith(applicationUser, prepayAmount)) { badgeAlerts.Add(PrepayBadgeMessage(Domain.Constants.Badges.HaveFaith, applicationUser.UserName)); }

            return badgeAlerts;
        }

        public List<string> ExtractOrderBadges(ApplicationUser applicationUser, Domain.Entities.DocumentDb.UserOrderHistory userOrderHistory, DateTime vendorClosingTime)
        {
            //CalculateStatistics(applicationUser, userOrderHistory);

            var badgeAlerts = new List<string>();
            if (AssignFirstOrder(applicationUser)) { badgeAlerts.Add(OrderBadgeMessage(Domain.Constants.Badges.FirstOrder)); }
            if (HighRoller(applicationUser)) { badgeAlerts.Add(OrderBadgeMessage(Domain.Constants.Badges.HighRoller)); }
            if (DeepPockets(applicationUser)) { badgeAlerts.Add(OrderBadgeMessage(Domain.Constants.Badges.DeepPockets)); }
            if (Consumer(applicationUser)) { badgeAlerts.Add(OrderBadgeMessage(Domain.Constants.Badges.Consumer)); }
            if (Enjoyer(applicationUser)) { badgeAlerts.Add(OrderBadgeMessage(Domain.Constants.Badges.Enjoyer)); }
            if (DieHard(applicationUser)) { badgeAlerts.Add(OrderBadgeMessage(Domain.Constants.Badges.DieHard)); }
            //if (Bankrupt(applicationUser)) { badgeAlerts.Add(OrderBadgeMessage(Domain.Constants.Badges.Bankrupt)); }
            if (Healthy(applicationUser)) { badgeAlerts.Add(OrderBadgeMessage(Domain.Constants.Badges.Healthy)); }
            if (PastaAddict(applicationUser)) { badgeAlerts.Add(OrderBadgeMessage(Domain.Constants.Badges.PastaAddict)); }

            if (CloseCall(applicationUser, userOrderHistory, Domain.Constants.Badges.CloseCall15, 0, 15, vendorClosingTime)) { badgeAlerts.Add(OrderBadgeMessage(Domain.Constants.Badges.CloseCall15)); };
            if (CloseCall(applicationUser, userOrderHistory, Domain.Constants.Badges.CloseCall30, 15, 30, vendorClosingTime)) { badgeAlerts.Add(OrderBadgeMessage(Domain.Constants.Badges.CloseCall30)); };
            if (CloseCall(applicationUser, userOrderHistory, Domain.Constants.Badges.CloseCall45, 30, 45, vendorClosingTime)) { badgeAlerts.Add(OrderBadgeMessage(Domain.Constants.Badges.CloseCall45)); };
            if (CloseCall(applicationUser, userOrderHistory, Domain.Constants.Badges.CloseCall60, 45, 60, vendorClosingTime)) { badgeAlerts.Add(OrderBadgeMessage(Domain.Constants.Badges.CloseCall60)); };

            // calculation needs to be done over previously earned badges
            if (BadgeCollector(applicationUser)) { badgeAlerts.Add(OrderBadgeMessage(Domain.Constants.Badges.BadgeCollector)); };

            return badgeAlerts;
        }

        private bool CloseCall(ApplicationUser applicationUser, Domain.Entities.DocumentDb.UserOrderHistory userOrderHistory, Badge badge, int startSeconds, int endSeconds, DateTime vendorClosingTime)
        {
            var closeCallBadge = applicationUser.Badges.FirstOrDefault(x => x.Id == badge.Id);
            if (CanEarnBadge(closeCallBadge, badge))
            {
                var diffInSeconds = vendorClosingTime.TimeOfDay - userOrderHistory.OrderTime.TimeOfDay;
                if (diffInSeconds.TotalSeconds > startSeconds && diffInSeconds.TotalSeconds <= endSeconds)
                {
                    if (closeCallBadge == null)
                    {
                        var closeCallBadgeMap = MapBadge(badge);
                        closeCallBadgeMap.TimesEarned += 1;
                        applicationUser.Badges.Add(closeCallBadgeMap);
                    }
                    else
                    {
                        closeCallBadge.TimesEarned += 1;
                    }
                    return true;
                }
            }

            return false;
        }

        private bool Bankrupt(ApplicationUser applicationUser)
        {
            var bankruptBadge = applicationUser.Badges.FirstOrDefault(x => x.Id == Domain.Constants.Badges.Bankrupt.Id);
            if (CanEarnBadge(bankruptBadge, Domain.Constants.Badges.Bankrupt))
            {
                if (applicationUser.Balance == 0)
                {
                    if (bankruptBadge == null)
                    {
                        var bankruptBadgeMap = MapBadge(Domain.Constants.Badges.Bankrupt);
                        bankruptBadgeMap.TimesEarned += 1;
                        applicationUser.Badges.Add(bankruptBadgeMap);
                    }
                    else
                    {
                        bankruptBadge.TimesEarned += 1;
                    }
                    return true;
                }
            }

            return false;
        }

        private bool PastaAddict(ApplicationUser applicationUser)
        {
            var pastaBadge = applicationUser.Badges.FirstOrDefault(x => x.Id == Domain.Constants.Badges.PastaAddict.Id);
            if (CanEarnBadge(pastaBadge, Domain.Constants.Badges.PastaAddict))
            {
                var yearlyStats = applicationUser.Statistics.YearlyTotals.FirstOrDefault();
                if (yearlyStats != null)
                {
                    if (applicationUser.Statistics.YearlyPastas > 52 && !yearlyStats.HasYearlyPastaBadge)
                    {
                        var pastaBadgeMap = MapBadge(Domain.Constants.Badges.PastaAddict);
                        pastaBadgeMap.TimesEarned += 1;
                        yearlyStats.HasYearlyPastaBadge = true;
                        applicationUser.Badges.Add(pastaBadgeMap);
                        return true;
                    }
                }
            }

            return false;
        }

        private bool Consumer(ApplicationUser applicationUser)
        {
            var consumerBadge = applicationUser.Badges.FirstOrDefault(x => x.Id == Domain.Constants.Badges.Consumer.Id);
            if (CanEarnBadge(consumerBadge, Domain.Constants.Badges.Consumer))
            {
                if (applicationUser.Statistics.AppTotalSpend > 500)
                {
                    var consumerBadgeMap = MapBadge(Domain.Constants.Badges.Consumer);
                    consumerBadgeMap.TimesEarned += 1;
                    applicationUser.Badges.Add(consumerBadgeMap);
                    return true;
                }
            }

            return false;
        }

        private bool Enjoyer(ApplicationUser applicationUser)
        {
            var enjoyerBadge = applicationUser.Badges.FirstOrDefault(x => x.Id == Domain.Constants.Badges.Enjoyer.Id);
            if (CanEarnBadge(enjoyerBadge, Domain.Constants.Badges.Enjoyer))
            {
                if (applicationUser.Statistics.AppTotalSpend > 1500)
                {
                    var enjoyerBadgeMap = MapBadge(Domain.Constants.Badges.Enjoyer);
                    enjoyerBadgeMap.TimesEarned += 1;
                    applicationUser.Badges.Add(enjoyerBadgeMap);
                    return true;
                }
            }

            return false;
        }


        private bool DieHard(ApplicationUser applicationUser)
        {
            var dieHardBadge = applicationUser.Badges.FirstOrDefault(x => x.Id == Domain.Constants.Badges.DieHard.Id);
            if (CanEarnBadge(dieHardBadge, Domain.Constants.Badges.DieHard))
            {
                if (applicationUser.Statistics.AppTotalSpend > 3000)
                {
                    var diehardBadgeMap = MapBadge(Domain.Constants.Badges.DieHard);
                    diehardBadgeMap.TimesEarned += 1;
                    applicationUser.Badges.Add(diehardBadgeMap);
                    return true;
                }
            }

            return false;
        }

        private bool Healthy(ApplicationUser applicationUser)
        {
            var hasHealthy = applicationUser.Badges.FirstOrDefault(x => x.Id == Domain.Constants.Badges.Healthy.Id);
            if (CanEarnBadge(hasHealthy, Domain.Constants.Badges.Healthy))
            {
                // be backwards compatible with migration
                foreach (var weeklyTotal in applicationUser.Statistics.WeeklyTotals)
                {
                    if (weeklyTotal.HealthyOrderCount >= 3 && !weeklyTotal.HasHealthyBadge)
                    {
                        if (hasHealthy != null)
                        {
                            hasHealthy.TimesEarned += 1;
                        }
                        else
                        {
                            var healthyBadgeMap = MapBadge(Domain.Constants.Badges.Healthy);
                            healthyBadgeMap.TimesEarned += 1;
                            applicationUser.Badges.Add(healthyBadgeMap);
                        }

                        weeklyTotal.HasHealthyBadge = true;
                        return true;
                    }
                }
            }

            return false;
        }

        private bool DeepPockets(ApplicationUser applicationUser)
        {
            var hasDeepPockets = applicationUser.Badges.FirstOrDefault(x => x.Id == Domain.Constants.Badges.DeepPockets.Id);
            if (CanEarnBadge(hasDeepPockets, Domain.Constants.Badges.DeepPockets))
            {
                // be backwards compatible with migration
                foreach (var monthlyTotal in applicationUser.Statistics.MonthlyTotals)
                {
                    if (monthlyTotal.Amount > 75 && !monthlyTotal.HasDeepPocketsBadge)
                    {
                        if (hasDeepPockets != null)
                        {
                            hasDeepPockets.TimesEarned += 1;
                        }
                        else
                        {
                            var deepPocketsBadgeMap = MapBadge(Domain.Constants.Badges.DeepPockets);
                            deepPocketsBadgeMap.TimesEarned += 1;
                            applicationUser.Badges.Add(deepPocketsBadgeMap);
                        }

                        monthlyTotal.HasDeepPocketsBadge = true;
                        return true;
                    }
                }
            }

            return false;
        }

        private bool HighRoller(ApplicationUser applicationUser)
        {
            var highRollerBadge = applicationUser.Badges.FirstOrDefault(x => x.Id == Domain.Constants.Badges.HighRoller.Id);
            if (CanEarnBadge(highRollerBadge, Domain.Constants.Badges.HighRoller))
            {
                // be backwards compatible with migration
                foreach (var weeklyTotal in applicationUser.Statistics.WeeklyTotals)
                {
                    if (weeklyTotal.Amount > 30 && !weeklyTotal.HasHighRollerBadge)
                    {
                        if (highRollerBadge != null)
                        {
                            highRollerBadge.TimesEarned += 1;
                        }
                        else
                        {
                            var highRollerBadgeMap = MapBadge(Domain.Constants.Badges.HighRoller);
                            highRollerBadgeMap.TimesEarned += 1;
                            applicationUser.Badges.Add(highRollerBadgeMap);
                        }

                        weeklyTotal.HasHighRollerBadge = true;
                        return true;
                    }
                }

            }

            return false;
        }

        private bool AssignFirstOrder(ApplicationUser applicationUser)
        {
            var firstOrderBadge = applicationUser.Badges.FirstOrDefault(x => x.Id == Domain.Constants.Badges.FirstOrder.Id);
            if (CanEarnBadge(firstOrderBadge, Domain.Constants.Badges.FirstOrder))
            {
                if (applicationUser.Statistics.AppTotalSpend > 0)
                {
                    var firstOrderBadgeMap = MapBadge(Domain.Constants.Badges.FirstOrder);
                    firstOrderBadgeMap.TimesEarned += 1;
                    applicationUser.Badges.Add(firstOrderBadgeMap);
                    return true;
                }
            }

            return false;
        }

        private bool HaveFaith(ApplicationUser applicationUser, decimal prepayAmount)
        {
            var haveFaithBadge = applicationUser.Badges.FirstOrDefault(x => x.Id == Domain.Constants.Badges.HaveFaith.Id);
            if (CanEarnBadge(haveFaithBadge, Domain.Constants.Badges.HaveFaith))
            {
                if (prepayAmount >= 100)
                {
                    if (haveFaithBadge != null)
                    {
                        haveFaithBadge.TimesEarned += 1;
                    }
                    else
                    {
                        var haveFaithBadgeMap = MapBadge(Domain.Constants.Badges.HaveFaith);
                        haveFaithBadgeMap.TimesEarned += 1;
                        applicationUser.Badges.Add(haveFaithBadgeMap);
                    }

                    return true;
                }
            }

            return false;
        }


        private bool BadgeCollector(ApplicationUser applicationUser)
        {
            var collectorBadge = applicationUser.Badges.FirstOrDefault(x => x.Id == Domain.Constants.Badges.BadgeCollector.Id);

            if (CanEarnBadge(collectorBadge, Domain.Constants.Badges.BadgeCollector))
            {
                var totalBadges = 0;
                foreach (var b in applicationUser.Badges)
                {
                    totalBadges += b.TimesEarned;
                }

                if (totalBadges > 10)
                {
                    var badgeCollectorMap = MapBadge(Domain.Constants.Badges.BadgeCollector);
                    badgeCollectorMap.TimesEarned += 1;
                    applicationUser.Badges.Add(badgeCollectorMap);
                    return true;
                }
            }

            return false;
        }

        private UserBadge MapBadge(Badge badge)
        {
            return _mapper.Map<Badge, UserBadge>(badge);
        }

        private bool CanEarnBadge(UserBadge userBadge, Badge badge)
        {
            return userBadge == null || badge.CanEarnMultipleTimes;
        }
    }
}