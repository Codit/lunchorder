using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lunchorder.Domain.Dtos;
using Lunchorder.Test.Integration.Helpers;
using Lunchorder.Test.Integration.Helpers.Base;
using NUnit.Framework;

namespace Lunchorder.Test.Integration.Repositories
{
    [TestFixture]
    public class DocumentDbRepositoryTest : RepositoryBase
    {
        [Test]
        public async Task SetActiveMenu()
        {
            var menu1 = new Domain.Entities.DocumentDb.Menu { Id = Guid.Parse(TestConstants.Menu.Menu1.Id), Enabled = true };
            var menu2 = new Domain.Entities.DocumentDb.Menu { Id = Guid.Parse(TestConstants.Menu.Menu2.Id), Enabled = false };

            await DatabaseRepository.SetActiveMenu(menu2.Id.ToString());
            var firstMenu = await DatabaseRepository.GetMenu(menu1.Id.ToString());
            Assert.NotNull(firstMenu);
            Assert.IsFalse(firstMenu.Enabled);
            var secondMenu = await DatabaseRepository.GetMenu(menu2.Id.ToString());
            Assert.NotNull(secondMenu);
            Assert.IsTrue(secondMenu.Enabled);
        }

        [Test]
        public async Task GetMenu_should_not_return_deleted()
        {
            var menu3Id = TestConstants.Menu.Menu3.Id;

            var menu3 = await DatabaseRepository.GetMenu(menu3Id);
            Assert.NotNull(menu3);
            Assert.IsFalse(menu3.Deleted);

            await DatabaseRepository.DeleteMenu(menu3Id);
            var deletedMenu = await DatabaseRepository.GetMenu(menu3Id);
            Assert.Null(deletedMenu);
        }

        [Test]
        public async Task AddMenu()
        {
            var menuVendor = new MenuVendor
            {
                Name = "'t kruimelken",
                Website = new Uri("http://www.tkruimelken.be/").ToString(),
                Logo = "",
                Address = new MenuVendorAddress
                {
                    Email = "info@tkruimelken.be",
                    City = "Ledeberg",
                    Phone = "09/232.04.53",
                    Fax = "09/232.04.54",
                    Street = "H.J. Reystraat",
                    StreetNumber = "1"
                },
                SubmitOrderTime = new TimeSpan(9, 0, 0).ToString()
            };

            var categoryBroodjesId = "5576c479-c37c-41c3-9c67-a464c656e002";
            var categoryKazenId = "8c60254c-1044-40f7-8740-cd1c09de824d";
            var categoryVisId = "cbe0c450-7b24-42fc-85be-f9e201ffba98";

            var menuCategories = new List<MenuCategory>
            {
                new MenuCategory
                {
                    Id =categoryBroodjesId,
                    Name = "Broodjes",
                    Description = "standaard met ei, tomaat, augurk, wortel, sla",
                    SubCategories= new List<MenuCategory>
                    {
                        new MenuCategory {
                        Id = categoryKazenId,
                        Name = "Kazen"
                        },
                        new MenuCategory
                        {
                        Id = categoryVisId,
                        Name = "Vis en schaaldieren"
                        }
                    }
                }
            };

            var menuEntries = new List<MenuEntry>()
            {
                new MenuEntry
                {
                    CategoryId = categoryKazenId,
                    Enabled = true,
                    Id = "d71e360b-f349-4bcf-b6ea-3eceb63832c7",
                    Picture = null,
                    Name = "Jonge kaas",
                    Price = "3,30"
                },
                new MenuEntry
                {
                    CategoryId = categoryKazenId,
                    Enabled = true,
                    Id = "5368da96-90df-49d8-aa78-39d9f4338eb5",
                    Picture = null,
                    Name = "Brie",
                    Price = "3,30"
                },
                new MenuEntry
                {
                    CategoryId = categoryKazenId,
                    Enabled = true,
                    Id = "d4cdae9b-ed06-4ec0-92fe-5624a4d8c45e",
                    Picture = null,
                    Name = "Philadelphia",
                    Price = "3,30"
                },
                new MenuEntry
                {
                    CategoryId = categoryKazenId,
                    Enabled = true,
                    Id = "b372f359-b4e4-437d-8303-cead6678287d",
                    Picture = null,
                    Name = "Boursin",
                    Price = "3,30"
                },
                new MenuEntry
                {
                    CategoryId = categoryKazenId,
                    Enabled = true,
                    Id = "f0417dae-73b4-46b6-bc75-cd435013b8af",
                    Picture = null,
                    Name = "Mozarella",
                    Price = "3,30"
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "2483e1ca-62fb-4160-a871-2b9659061238",
                    Picture = null,
                    Name = "Krabsalade",
                    Price = "3,30"
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "9906620c-3c67-4cb6-a223-20a3abf6be5e",
                    Picture = null,
                    Name = "Tonijnsalade",
                    Price = "3,80"
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "e328bfc4-55b4-4e95-ac9d-2a20d21d996a",
                    Picture = null,
                    Name = "Noordzeesalade",
                    Price = "3,30"
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "49f803f1-13a8-4502-b60d-4f815105927a",
                    Picture = null,
                    Name = "Zalmsalade",
                    Price = "3,80"
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "efb1de44-58fd-4aa6-95d3-895c2592384c",
                    Picture = null,
                    Name = "Garnaalsalade",
                    Price = "4,30"
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "b66b5c57-2e4a-4422-9221-cc44cbd4d9ed",
                    Picture = null,
                    Name = "haring-dille salade",
                    Price = "3,80"
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "4ea21005-8081-428f-a922-b1c880f3ef20",
                    Picture = null,
                    Name = "pikante tonijnsalade",
                    Price = "3,80"
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "d4d8eaa4-ec7d-42b7-92cf-9029b0b7768e",
                    Picture = null,
                    Name = "Gerookte zalm",
                    Price = "4,30"
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "236e2f12-6e34-40ec-8149-dad6596ce4e1",
                    Picture = null,
                    Name = "Scampi in de looksaus",
                    Price = "4,30"
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "df9ea7f8-bf19-456a-bb2a-4fc6c829f54e",
                    Picture = null,
                    Name = "Scampi-diabolique salade",
                    Price = "4,30"
                }
            };

            var menuRules = new List<MenuRule>
            {
                new MenuRule
                {
                    Id = "de50edcb-5ade-4d89-85eb-3ccc49181987",
                    Enabled = true,
                    Description = "Bruin brood",
                    PriceDelta = 0,
                    CategoryIds = new List<string> {categoryBroodjesId}
                },
                new MenuRule
                {
                    Id = "7cb010e7-7b5f-4a9f-a1cb-07171ef22262",
                    Enabled = true,
                    Description = "Meergranen brood",
                    PriceDelta = 0,
                    CategoryIds = new List<string> {categoryBroodjesId}
                }
            };

            var menu = new Menu
            {
                Id = Guid.NewGuid(),
                Enabled = true,
                Categories = menuCategories,
                Entries = menuEntries,
                Deleted = false,
                LastUpdated = DateTime.UtcNow,
                Revision = 1,
                Rules = menuRules,
                Vendor = menuVendor
            };

            await DatabaseRepository.AddMenu(menu);
            var dbMenu = await DatabaseRepository.GetMenu(menu.Id.ToString());
            Assert.NotNull(dbMenu);
        }

        [Test]
        public async Task UpdateMenu()
        {
            var menu = new Menu { Id = Guid.Parse(TestConstants.Menu.Menu2.Id), Revision = 123 };

            await DatabaseRepository.UpdateMenu(menu);
            var dbMenu = await DatabaseRepository.GetMenu(menu.Id.ToString());
            Assert.NotNull(dbMenu);
        }

        [Test]
        public async Task GetBadges()
        {
            var badges = await DatabaseRepository.GetBadges();
            Assert.IsNotNull(badges);
            var badgesList = badges.ToList();
            Assert.AreEqual(15, badgesList.Count);

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
