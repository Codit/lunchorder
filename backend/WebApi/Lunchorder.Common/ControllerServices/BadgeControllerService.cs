using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Constants;
using Lunchorder.Domain.Dtos;
using Lunchorder.Domain.Dtos.Responses;

namespace Lunchorder.Common.ControllerServices
{
    public class BadgeControllerService : IBadgeControllerService
    {
        private readonly IBadgeService _badgeService;
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IMenuService _menuService;

        public BadgeControllerService(IBadgeService badgeService, IDatabaseRepository databaseRepository, IMenuService menuService)
        {
            _badgeService = badgeService ?? throw new ArgumentNullException(nameof(badgeService));
            _menuService = menuService ?? throw new ArgumentNullException(nameof(menuService));
            _databaseRepository = databaseRepository;
        }

        public async Task<GetBadgesResponse> Get()
        {
            var response = new GetBadgesResponse
            {
                Badges = await Task.FromResult(Badges.BadgeList),
            };

            if (MemoryCacher.GetValue(Cache.BadgeRanking) is IEnumerable<BadgeRanking> cacheBadgeRankings)
            {
                response.BadgeRankings = cacheBadgeRankings;
            }
            else
            {
                var badgeRankings = await _databaseRepository.GetBadgeRanking();
                
                if (badgeRankings != null)
                    MemoryCacher.Add(Cache.BadgeRanking, badgeRankings, DateTimeOffset.UtcNow.AddDays(1));

                response.BadgeRankings = badgeRankings;
            }
            
            return response;
        }

        public async Task<IEnumerable<string>> SetOrderBadges(string username, string userId)
        {
            var applicationUser = await _databaseRepository.GetApplicationUser(username);
            var menu = await _menuService.GetActiveMenu();
            var lastOrder = await _databaseRepository.GetLastOrder(userId);
            var badges = _badgeService.ExtractOrderBadges(applicationUser, lastOrder, DateTime.Parse(menu.Vendor.SubmitOrderTime));
            _databaseRepository.SaveApplicationUser(applicationUser);
            _databaseRepository.SaveUserOrder(lastOrder);
            return badges;
        }

        public async Task<IEnumerable<string>> SetPrepayBadges(string username)
        {
            var applicationUser = await _databaseRepository.GetApplicationUser(username);
            var userAudit = await _databaseRepository.GetLastUserBalanceAudit(applicationUser.UserId);
            var prepayBadges = _badgeService.ExtractPrepayBadges(applicationUser, userAudit.Amount);
            _databaseRepository.SaveApplicationUser(applicationUser);
            return prepayBadges;
        }
    }
}