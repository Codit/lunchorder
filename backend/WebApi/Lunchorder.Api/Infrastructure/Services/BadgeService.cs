using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lunchorder.Domain.Dtos;
using Lunchorder.Domain.Entities.Authentication;
using Lunchorder.Domain.Entities.DocumentDb;
using UserBadge = Lunchorder.Domain.Entities.DocumentDb.UserBadge;
using UserOrderHistory = Lunchorder.Domain.Entities.DocumentDb.UserOrderHistory;

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

        public List<string> ExtractOrderBadges(ApplicationUser applicationUser, UserOrderHistory userOrderHistory, DateTime vendorClosingTime)
        {
            AssignFirstOrder(applicationUser);
            HighRoller(applicationUser);
            DeepPockets(applicationUser);
            Consumer(applicationUser);
            Enjoyer(applicationUser);
            DieHard(applicationUser);
            Bankrupt(applicationUser);
            Healthy(applicationUser);

            CloseCall(applicationUser, userOrderHistory, Domain.Constants.Badges.CloseCall15, 0, 15, vendorClosingTime);
            CloseCall(applicationUser, userOrderHistory, Domain.Constants.Badges.CloseCall30, 15, 30, vendorClosingTime);
            CloseCall(applicationUser, userOrderHistory, Domain.Constants.Badges.CloseCall45, 30, 45, vendorClosingTime);
            CloseCall(applicationUser, userOrderHistory, Domain.Constants.Badges.CloseCall60, 45, 60, vendorClosingTime);

            // calculation needs to be done over previously earned badges
            BadgeCollector(applicationUser);

            return new List<string>();
        }

        private bool CloseCall(ApplicationUser applicationUser, UserOrderHistory userOrderHistory, Badge badge, int startSeconds, int endSeconds, DateTime vendorClosingTime)
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

        public void ExtractPrepayBadges(ApplicationUser applicationUser, decimal prepayAmount)
        {
            HaveFaith(applicationUser, prepayAmount);

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
                    if (weeklyTotal.Amount > 75 && !weeklyTotal.HasHealthyBadge)
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
                    var haveFaithBadgeMap = MapBadge(Domain.Constants.Badges.HaveFaith);
                    haveFaithBadgeMap.TimesEarned += 1;
                    applicationUser.Badges.Add(haveFaithBadgeMap);
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
                if (applicationUser.Badges.Count > 10)
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

    public interface IBadgeService
    {
        List<string> ExtractOrderBadges(ApplicationUser applicationUser, UserOrderHistory userOrderHistory,
            DateTime vendorClosingTime);
    }
}