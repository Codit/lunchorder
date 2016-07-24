using System;
using System.Linq;
using System.Threading.Tasks;
using Lunchorder.Test.Integration.Helpers;
using Lunchorder.Test.Integration.Helpers.Base;
using NUnit.Framework;

namespace Lunchorder.Test.Integration.Repositories
{
    [TestFixture]
    public class DocumentDbRepositoryTest : RepositoryBase
    {
        [Test]
        public async Task GetBadges()
        {
            var badges = await DatabaseRepository.GetBadges();
            Assert.IsNotNull(badges);
            var badgesList = badges.ToList();
            Assert.AreEqual(5, badgesList.Count);

            var firstBadge = badgesList.FirstOrDefault(x => x.Id == Guid.Parse("5de5ecfd-6e4d-47e3-b129-96614e745ee5"));
            Assert.NotNull(firstBadge);
            Assert.AreEqual(firstBadge.Id.ToString(), "5de5ecfd-6e4d-47e3-b129-96614e745ee5");
            Assert.AreEqual(firstBadge.Name, "Close Call (15 sec)");
            Assert.AreEqual(firstBadge.Icon, "close_call_15sec_200x200.png");
            Assert.AreEqual(firstBadge.Description, "Make an order 15 seconds before all orders are sent");
        }

        /// <summary>
        /// We retrieve users by username, not by Id
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetUserInfo_ById_Should_Return_Null()
        {
            var userInfo = await DatabaseRepository.GetUserInfo(TestConstants.User1.Id);
            Assert.Null(userInfo);
        }

        [Test]
        public async Task GetUserInfo()
        {
            var userInfo = await DatabaseRepository.GetUserInfo(TestConstants.User1.Username);
            Assert.IsNotNull(userInfo);

            Assert.IsNotNull(userInfo.Badges);
            var userBadges = userInfo.Badges.ToList();
            Assert.AreEqual(1, userBadges.Count);
            var firstUserBadge = userBadges.First();
            Assert.AreEqual(TestConstants.Badges.Badge1.Id, firstUserBadge.BadgeId.ToString());
            Assert.AreEqual(2, firstUserBadge.TimesEarned);

            Assert.IsNotNull(userInfo.Favorites);
            var userFavorites = userInfo.Favorites.ToList();
            Assert.AreEqual(3, userFavorites.Count);
            var firstUserFavorite = userFavorites.First();
            Assert.AreEqual(TestConstants.Favorites.Favorite1.MenuEntryId, firstUserFavorite.MenuEntryId.ToString());
            var secondUserFavorite = userFavorites.First();
            Assert.AreEqual(TestConstants.Favorites.Favorite1.MenuEntryId, secondUserFavorite.MenuEntryId.ToString());
            var thirdUserFavorite = userFavorites.First();
            Assert.AreEqual(TestConstants.Favorites.Favorite1.MenuEntryId, thirdUserFavorite.MenuEntryId.ToString());

            Assert.IsNotNull(userInfo.Profile);
            Assert.AreEqual(TestConstants.User1.Profile.FirstName, userInfo.Profile.FirstName);
            Assert.AreEqual(TestConstants.User1.Profile.LastName, userInfo.Profile.LastName);
            Assert.AreEqual(TestConstants.User1.Profile.Culture, userInfo.Profile.Culture);
            Assert.AreEqual(TestConstants.User1.Profile.Picture, userInfo.Profile.Picture);
        }
    }
}
