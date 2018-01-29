using System;
using System.Collections.Generic;
using Lunchorder.Domain.Entities.Authentication;
using Lunchorder.Domain.Entities.DocumentDb;

namespace Lunchorder.Common.Interfaces
{
    public interface IBadgeService
    {
        List<string> ExtractOrderBadges(ApplicationUser applicationUser, UserOrderHistory userOrderHistory,
            DateTime vendorClosingTime);

        List<string> ExtractPrepayBadges(ApplicationUser applicationUser, decimal prepayAmount);
    }
}