using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Lunchorder.Common;
using Lunchorder.Domain.Dtos;
using Lunchorder.Test.Integration.Helpers;
using Lunchorder.Test.Integration.Helpers.Base;
using Microsoft.Azure.Documents;
using NUnit.Framework;
using Menu = Lunchorder.Domain.Dtos.Menu;
using MenuCategory = Lunchorder.Domain.Dtos.MenuCategory;
using MenuEntry = Lunchorder.Domain.Dtos.MenuEntry;
using MenuRule = Lunchorder.Domain.Dtos.MenuRule;
using MenuVendor = Lunchorder.Domain.Dtos.MenuVendor;
using MenuVendorAddress = Lunchorder.Domain.Dtos.MenuVendorAddress;
using UserOrderHistory = Lunchorder.Domain.Dtos.UserOrderHistory;
using UserOrderHistoryEntry = Lunchorder.Domain.Dtos.UserOrderHistoryEntry;

namespace Lunchorder.Test.Integration.Repositories
{
    [TestFixture]
    public class DocumentDbRepositoryTest : RepositoryBase
    {
        // todo add test to check audit document creation
        // todo add test to check autit document update

        [Test]
        public async Task InsertUniquePushToken()
        {
            var token = "token123";
            var userId = TestConstants.User3.Id;

            await DatabaseRepository.StorePushToken(token, userId);
            var pushTokens = (await DatabaseRepository.GetPushTokens()).ToList();
            Assert.AreEqual(3, pushTokens.Count());

            var pushToken = pushTokens.SingleOrDefault(x => x.Token == token && x.UserId == userId);
            Assert.NotNull(pushToken);
        }

        [Test]
        public async Task UpdatePushToken()
        {
            var token = "token123456";
            var userId = TestConstants.User1.Id;

            await DatabaseRepository.StorePushToken(token, userId);
            var pushTokens = (await DatabaseRepository.GetPushTokens()).ToList();

            Assert.AreEqual(3, pushTokens.Count());
            var pushToken = pushTokens.SingleOrDefault(x => x.Token == token && x.UserId == userId);
            Assert.NotNull(pushToken);
        }

        [Test]
        public async Task UpgradeUserHistory()
        {
            var user = await DatabaseRepository.GetUserInfo(TestConstants.User1.UserName);
            Assert.AreEqual(0, user.Last5BalanceAuditItems.Count());
            await DatabaseRepository.UpgradeUserHistory();
            user = await DatabaseRepository.GetUserInfo(TestConstants.User1.UserName);
            Assert.AreEqual(5, user.Last5BalanceAuditItems.Count());
            var user2 = await DatabaseRepository.GetUserInfo(TestConstants.User2.UserName);
            Assert.AreEqual(0, user2.Last5BalanceAuditItems.Count());
        }

        [Test]
        public async Task UpdateUserImage()
        {
            var newPicture = "http://some.pic.tu.re";
            var user = await DatabaseRepository.GetUserInfo(TestConstants.User3.UserName);
            Assert.AreEqual(TestConstants.User3.Picture, user.Profile.Picture);
            await DatabaseRepository.UpdateUserPicture(TestConstants.User3.Id, newPicture);
            user = await DatabaseRepository.GetUserInfo(TestConstants.User3.UserName);
            Assert.AreEqual(newPicture, user.Profile.Picture);
        }

        [Test]
        public async Task AddToUserList()
        {
            await DatabaseRepository.AddToUserList(TestConstants.User1.Id, TestConstants.User1.UserName, TestConstants.User1.FirstName, TestConstants.User1.LastName);
            var users = await DatabaseRepository.GetUsers();
            Assert.NotNull(users);
            var usersList = users.ToList();
            Assert.AreEqual(1, usersList.Count);
            var firstUser = usersList.First();
            AssertUsers(firstUser, TestConstants.User1.Id, TestConstants.User1.UserName, TestConstants.User1.FirstName,
                TestConstants.User1.LastName);

            // also test update
            await DatabaseRepository.AddToUserList(TestConstants.User2.Id, TestConstants.User2.UserName, TestConstants.User2.FirstName, TestConstants.User2.LastName);
            users = await DatabaseRepository.GetUsers();
            Assert.NotNull(users);
            usersList = users.ToList();
            Assert.AreEqual(2, usersList.Count);
            firstUser = usersList.First();
            AssertUsers(firstUser, TestConstants.User1.Id, TestConstants.User1.UserName, TestConstants.User1.FirstName,
                TestConstants.User1.LastName);
            var secondUser = usersList[1];
            AssertUsers(secondUser, TestConstants.User2.Id, TestConstants.User2.UserName, TestConstants.User2.FirstName,
                TestConstants.User2.LastName);
        }

        private void AssertUsers(PlatformUserListItem user, string userId, string userName, string firstName, string lastName)
        {
            Assert.AreEqual(userId, user.UserId);
            Assert.AreEqual(userName,  user.UserName);
            Assert.AreEqual(firstName, user.FirstName);
            Assert.AreEqual(lastName,  user.LastName);
        }

        [Test]
        public async Task UpdateBalance()
        {
            var originator = new SimpleUser
            {
                Id = TestConstants.User4.Id,
                UserName = TestConstants.User4.UserName
            };

            var amount = 100.01M;
            var userId = TestConstants.User3.Id;
            var updatedBalance = await DatabaseRepository.UpdateBalance(userId, amount, originator);
            Assert.AreEqual(TestConstants.User3.Balance + amount, updatedBalance);
        }

        [Test]
        public async Task AddOrder_Should_Fail_When_Insufficient_Balance()
        {
            var userId = TestConstants.User3.Id;
            var userName = TestConstants.User3.UserName;

            var vendorId = Guid.NewGuid().ToString();

            var userOrderHistoryEntries = new List<UserOrderHistoryEntry>
            {
                new UserOrderHistoryEntry
                {
                    Id = Guid.NewGuid(),
                    MenuEntryId = Guid.NewGuid(),
                    Price = 5,
                    Rules = new List<UserOrderHistoryRule>
                    {
                        new UserOrderHistoryRule
                        {
                            Description = "No vegetables",
                            Id = Guid.NewGuid(),
                            PriceDelta = 0.35M
                        }
                    },
                    Name = "test item"
                },
            };

            var orderDate = new DateGenerator().GenerateDateFormat(DateTime.UtcNow);

            var userOrderHistory = new UserOrderHistory { Entries = userOrderHistoryEntries, OrderTime = DateTime.UtcNow };

            // Add an order and it should create 2 entries
            Assert.Throws<DocumentClientException>(async () => await DatabaseRepository.AddOrder(userId, userName, vendorId, orderDate, userOrderHistory, TestConstants.User3.FullName));

            // todo thank you document db that we cannot parse the exception as it's invalid json... too much work for now
        }

        [Test]
        public async Task AddOrder_Should_CreateNewEntries()
        {
            var userId = TestConstants.User1.Id;
            var userName = TestConstants.User1.UserName;

            var vendorId = Guid.NewGuid().ToString();

            var userOrderHistoryEntries = new List<UserOrderHistoryEntry>
            {
                new UserOrderHistoryEntry
                {
                    Id = Guid.NewGuid(),
                    MenuEntryId = Guid.NewGuid(),
                    Price = 5,
                    Rules = new List<UserOrderHistoryRule>
                    {
                        new UserOrderHistoryRule
                        {
                            Description = "No vegetables",
                            Id = Guid.NewGuid(),
                            PriceDelta = 0.35M
                        }
                    },
                    Name = "test item"
                },
                new UserOrderHistoryEntry
                {
                    Id = Guid.NewGuid(),
                    MenuEntryId = Guid.NewGuid(),
                    Price = 10,
                    Rules = null,
                    Name = "test item 2"
                }
            };

            var orderDate = new DateGenerator().GenerateDateFormat(DateTime.UtcNow);

            var userOrderHistory = new UserOrderHistory { Entries = userOrderHistoryEntries, OrderTime = DateTime.UtcNow };

            // Add an order and it should create 2 entries
            await DatabaseRepository.AddOrder(userId, userName, vendorId, orderDate, userOrderHistory, TestConstants.User1.FullName);
            var vendorOrderHistory = await DatabaseRepository.GetVendorOrder(orderDate, vendorId);
            Assert.AreNotEqual(new Guid().ToString(), vendorOrderHistory.VendorId);
            Assert.NotNull(vendorOrderHistory.Entries);
            var entryList = vendorOrderHistory.Entries.ToList();
            Assert.AreEqual(2, entryList.Count);
            var firstEntry = entryList[0];
            Assert.AreEqual(userOrderHistoryEntries[0].Name, firstEntry.Name);
            Assert.AreEqual(5.35d, firstEntry.FinalPrice);
            var secondEntry = entryList[1];
            Assert.AreEqual(userOrderHistoryEntries[1].Name, secondEntry.Name);
            Assert.AreEqual(userOrderHistoryEntries[1].Price, secondEntry.FinalPrice);

            var userInfo = await DatabaseRepository.GetUserInfo(userName);
            Assert.NotNull(userInfo);
            Assert.NotNull(userInfo.Last5Orders);
            var last5OrdersList = userInfo.Last5Orders.ToList();
            Assert.AreEqual(1, last5OrdersList.Count);
            Assert.NotNull(last5OrdersList[0].LastOrderEntries);
            Assert.AreEqual(userOrderHistory.OrderTime, last5OrdersList[0].OrderTime);
            Assert.AreEqual(15.35, last5OrdersList[0].FinalPrice);
            var lastOrderEntriesList = last5OrdersList[0].LastOrderEntries.ToList();
            Assert.AreEqual(userOrderHistoryEntries.Count, lastOrderEntriesList.Count);
            var firstLastOrderEntry = lastOrderEntriesList[0];
            Assert.AreEqual(userOrderHistoryEntries[0].Name, firstLastOrderEntry.Name);
            Assert.AreEqual(userOrderHistoryEntries[0].Price, firstLastOrderEntry.Price);
            Assert.AreEqual(string.Join("\n", userOrderHistoryEntries[0].Rules.Select(x => x.Description)), firstLastOrderEntry.AppliedRules);
            Assert.AreEqual(35.15, userInfo.Balance);

        }

        [Test]
        public async Task AddOrder_Should_UseExisting_VendorOrderHistory()
        {
            var userId = TestConstants.User2.Id;
            var userName = TestConstants.User2.UserName;
            var vendorId = TestConstants.VendorOrderHistory.VendorOrderHistory1.VendorId;
            var vendorOrderHistoryId = TestConstants.VendorOrderHistory.VendorOrderHistory1.Id;

            var userOrderHistoryEntries = new List<UserOrderHistoryEntry>
            {
                new UserOrderHistoryEntry
                {
                    Id = Guid.NewGuid(),
                    MenuEntryId = Guid.NewGuid(),
                    Price = 5,
                    Rules = null,
                    Name = "test item"
                }
            };

            var orderDate = new DateGenerator().GenerateDateFormat(TestConstants.VendorOrderHistory.VendorOrderHistory1.OrderDate);

            var userOrderHistory = new UserOrderHistory { Entries = userOrderHistoryEntries, OrderTime = DateTime.UtcNow };

            // There is an existing order in the database (seed)
            var vendorOrderHistory = await DatabaseRepository.GetVendorOrder(orderDate, vendorId);
            Assert.IsNotNull(vendorOrderHistory);

            // Add an order and it should use the existing vendor order history
            await DatabaseRepository.AddOrder(userId, userName, vendorId, orderDate, userOrderHistory, TestConstants.User2.FullName);
            vendorOrderHistory = await DatabaseRepository.GetVendorOrder(orderDate, vendorId);
            Assert.NotNull(vendorOrderHistory);
            Assert.AreEqual(vendorId, vendorOrderHistory.VendorId);
            Assert.AreEqual(vendorOrderHistoryId, vendorOrderHistory.Id);
        }

        [Test]
        public async Task AddOrder_Should_Create_A_New_VendorOrderHistory()
        {
            var userId = TestConstants.User2.Id;
            var userName = TestConstants.User2.UserName;
            var vendorId = Guid.NewGuid().ToString();

            var userOrderHistoryEntries = new List<UserOrderHistoryEntry>
            {
                new UserOrderHistoryEntry
                {
                    Id = Guid.NewGuid(),
                    MenuEntryId = Guid.NewGuid(),
                    Price = 5,
                    Rules = null,
                    Name = "test item"
                }
            };
            var orderDate = new DateGenerator().GenerateDateFormat(DateTime.UtcNow.AddDays(7));

            var userOrderHistory = new UserOrderHistory { Entries = userOrderHistoryEntries, OrderTime = DateTime.UtcNow };

            // There should be no vendor order history for next week
            var vendorOrderHistory = await DatabaseRepository.GetVendorOrder(orderDate, vendorId);
            Assert.IsNull(vendorOrderHistory);

            // Add an order and there should be a new vendor order history
            await DatabaseRepository.AddOrder(userId, userName, vendorId, orderDate, userOrderHistory, TestConstants.User2.FullName);
            vendorOrderHistory = await DatabaseRepository.GetVendorOrder(orderDate, vendorId);
            Assert.NotNull(vendorOrderHistory);
            Assert.AreEqual(vendorOrderHistory.VendorId.ToString(), vendorId);
        }

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
                Id = "34b19c20-395c-4811-a532-b91469afc5ac",
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
            var categoryVleeswarenId = "7aaa919d-71a1-4b24-8ee1-1fa68b3ae1c7";
            var categoryAndereId = "7120557d-9b3a-4c00-bc6a-a7b040e5fe07";

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
                        },
                         new MenuCategory
                        {
                        Id = categoryVleeswarenId,
                        Name = "Vleeswaren"
                        },
                         new MenuCategory
                         {
                             Id = categoryAndereId,
                             Name = "Andere"
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
                    Price = 3.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryKazenId,
                    Enabled = true,
                    Id = "5368da96-90df-49d8-aa78-39d9f4338eb5",
                    Picture = null,
                    Name = "Brie",
                    Price = 3.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryKazenId,
                    Enabled = true,
                    Id = "d4cdae9b-ed06-4ec0-92fe-5624a4d8c45e",
                    Picture = null,
                    Name = "Philadelphia",
                    Price = 3.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryKazenId,
                    Enabled = true,
                    Id = "b372f359-b4e4-437d-8303-cead6678287d",
                    Picture = null,
                    Name = "Boursin",
                    Price = 3.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryKazenId,
                    Enabled = true,
                    Id = "f0417dae-73b4-46b6-bc75-cd435013b8af",
                    Picture = null,
                    Name = "Mozarella",
                    Price = 3.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "2483e1ca-62fb-4160-a871-2b9659061238",
                    Picture = null,
                    Name = "Krabsalade",
                    Price = 3.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "9906620c-3c67-4cb6-a223-20a3abf6be5e",
                    Picture = null,
                    Name = "Tonijnsalade",
                    Price = 3.80M
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "e328bfc4-55b4-4e95-ac9d-2a20d21d996a",
                    Picture = null,
                    Name = "Noordzeesalade",
                    Price = 3.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "49f803f1-13a8-4502-b60d-4f815105927a",
                    Picture = null,
                    Name = "Zalmsalade",
                    Price = 3.80M
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "efb1de44-58fd-4aa6-95d3-895c2592384c",
                    Picture = null,
                    Name = "Garnaalsalade",
                    Price = 4.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "b66b5c57-2e4a-4422-9221-cc44cbd4d9ed",
                    Picture = null,
                    Name = "haring-dille salade",
                    Price = 3.80M
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "4ea21005-8081-428f-a922-b1c880f3ef20",
                    Picture = null,
                    Name = "pikante tonijnsalade",
                    Price = 3.80M
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "d4d8eaa4-ec7d-42b7-92cf-9029b0b7768e",
                    Picture = null,
                    Name = "Gerookte zalm",
                    Price = 4.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "236e2f12-6e34-40ec-8149-dad6596ce4e1",
                    Picture = null,
                    Name = "Scampi in de looksaus",
                    Price = 4.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryVisId,
                    Enabled = true,
                    Id = "df9ea7f8-bf19-456a-bb2a-4fc6c829f54e",
                    Picture = null,
                    Name = "Scampi-diabolique salade",
                    Price = 4.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryVleeswarenId,
                    Enabled = true,
                    Id = "5dcb1472-33ae-419c-95f4-b4d3b6ffae31",
                    Picture = null,
                    Name = "Hesp",
                    Price = 3.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryVleeswarenId,
                    Enabled = true,
                    Id = "c69e17e1-8578-436a-9048-c51bce5a06f4",
                    Picture = null,
                    Name = "Préparé",
                    Price = 3.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryVleeswarenId,
                    Enabled = true,
                    Id = "be9980c4-a19f-47bf-97ad-ec98d6cddbce",
                    Picture = null,
                    Name = "Varkensgebraad",
                    Price = 3.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryVleeswarenId,
                    Enabled = true,
                    Id = "9cd02feb-6c40-4dbe-97ec-ac501f789d86",
                    Picture = null,
                    Name = "Salami",
                    Price = 3.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryVleeswarenId,
                    Enabled = true,
                    Id = "0d9fa498-aad5-493d-9431-83a4137f016c",
                    Picture = null,
                    Name = "Frikandon",
                    Price = 3.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryVleeswarenId,
                    Enabled = true,
                    Id = "49cd466d-7477-439c-9446-48895dfc16ab",
                    Picture = null,
                    Name = "Kip salade",
                    Price = 3.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryVleeswarenId,
                    Enabled = true,
                    Id = "f76281d8-0d62-4899-9bbb-2aee9db1bb17",
                    Picture = null,
                    Name = "Kip curry",
                    Price = 3.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryVleeswarenId,
                    Enabled = true,
                    Id = "713ac2d0-3abc-4147-9e80-38465a819af5",
                    Picture = null,
                    Name = "Kip samourai salade",
                    Price = 3.80M
                },
                new MenuEntry
                {
                    CategoryId = categoryVleeswarenId,
                    Enabled = true,
                    Id = "af75f6ce-c4df-46fd-a1bf-7f2791c75c25",
                    Picture = null,
                    Name = "Kip in pepersaus",
                    Price = 3.80M
                },
                new MenuEntry
                {
                    CategoryId = categoryVleeswarenId,
                    Enabled = true,
                    Id = "deda0c26-eee1-4da7-8d12-ddc8c6725ab9",
                    Picture = null,
                    Name = "Ham-prei salade",
                    Price = 3.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryVleeswarenId,
                    Enabled = true,
                    Id = "3f4349c1-6c65-43d3-9d57-7f18208c1b33",
                    Picture = null,
                    Name = "Breydelhamsalade",
                    Price = 3.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryVleeswarenId,
                    Enabled = true,
                    Id = "d1c14454-2405-4108-8c79-723dee975d81",
                    Picture = null,
                    Name = "Italiaanse ham",
                    Price = 3.80M
                },
                new MenuEntry
                {
                    CategoryId = categoryAndereId,
                    Enabled = true,
                    Id = "24fe0f4e-6942-4391-b8b9-8045d5399698",
                    Picture = null,
                    Name = "Eiersalade",
                    Price = 3.30M
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
        public async Task AddBiteMeMenu()
        {
            var menuVendor = new MenuVendor
            {
                Id = "386ae78f-6ac1-4fda-a44f-e74c4e1e9197",
                Name = "Bite me",
                Website = null,
                Logo = "",
                Address = new MenuVendorAddress
                {
                    Email = "jonas.vanderbiest@telenet.be",
                    City = string.Empty,
                    Phone = "09/324.68.94",
                    Fax = "09/279.85.43",
                    Street = string.Empty,
                    StreetNumber = string.Empty
                },
                SubmitOrderTime = new DateTime(1977, 1, 1, 7, 30, 0).ToString(CultureInfo.InvariantCulture),
                // todo, add some closing date ranges.
                //ClosingDateRanges = new List<>
            };

            var categoryBroodjesId = "83af0051-c407-4936-a8f6-e1e292b992ed";
            var categoryKoudePastaSalades1 = "d3ba9465-d154-41dd-adaa-a361d7a3511c";
            var categoryKoudePastaSalades2 = "f878b0f8-5acc-445a-8e72-f75eca89e7ca";
            var categoryWarmePasta1 = "9a4aa3d7-c0b2-470b-b262-32dad01a7bad";
            var categoryWarmePasta2 = "7d943d15-4849-4ba3-9a7b-9a00043bcbf1";
            var categoryCroques = "415a8daa-fa3b-4e41-ae87-bbc1b9b1fbc9";
            var categorySalades1 = "afcc4fb9-37bb-4d47-ad09-90667aea0098";
            var categorySalades2 = "618767d5-8a79-4d0c-b39c-c5e2656fe23f";
            var categorySalades3 = "5e1fcc06-8ea3-4241-a0fc-90d43b1e5b83";
            var categorySoep = "7b1e7d55-c3d6-4479-b6e8-23d95cb2d628";

            var menuCategories = new List<MenuCategory>
            {
                new MenuCategory
                {
                    Id =categoryBroodjesId,
                    Name = "Broodjes",
                    Description = string.Empty
                },
                new MenuCategory
                {
                    Id =categoryKoudePastaSalades1,
                    Name = "Koude pasta salades 1",
                    Description = string.Empty
                },
                new MenuCategory
                {
                    Id =categoryKoudePastaSalades2,
                    Name = "Koude pasta salades 2",
                    Description = string.Empty
                },
                new MenuCategory
                {
                    Id =categoryWarmePasta1,
                    Name = "Warme pasta 1",
                    Description = string.Empty
                },
                new MenuCategory
                {
                    Id =categoryWarmePasta2,
                    Name = "Warme pasta 2",
                    Description = string.Empty
                },
                new MenuCategory
                {
                    Id =categoryCroques,
                    Name = "Croques",
                    Description = string.Empty
                },
                new MenuCategory
                {
                    Id =categorySalades1,
                    Name = "Salades 1",
                    Description = string.Empty
                },
                new MenuCategory
                {
                    Id =categorySalades2,
                    Name = "Salades 2",
                    Description = string.Empty
                },
                new MenuCategory
                {
                    Id =categorySalades3,
                    Name = "Salades 3",
                    Description = string.Empty
                },
                new MenuCategory
                {
                    Id =categorySoep,
                    Name = "Soep",
                    Description = string.Empty
                }
            };

            var menuEntries = new List<MenuEntry>
            {
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "b6b595b8-8eb2-4e20-9dea-babd59622626",
                    Picture = null,
                    Name = "Kaas 1: mosterd peterselie",
                    Price = 2.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "4a08fbd8-b3a8-496b-a388-bc2db528865a",
                    Picture = null,
                    Name = "Kaas 2: boter",
                    Price = 2.20M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "e0d2f0ca-105e-460b-bf37-cbb85eedbd70",
                    Picture = null,
                    Name = "Hesp, boter",
                    Price = 2.20M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "543ae6a7-8bfa-448c-9f7f-f290f4e260f0",
                    Picture = null,
                    Name = "Kaas en hesp",
                    Price = 2.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "a5a0bbb3-dc6f-43ee-aa8e-bf50d587fe37",
                    Picture = null,
                    Name = "Salami",
                    Price = 2.20M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "8e200655-c90f-48e9-85b6-eb63d438a8fe",
                    Picture = null,
                    Name = "Préparé",
                    Price = 2.50M
                }
                ,new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "49ea1d3b-8f95-4cbb-a099-548751326fb4",
                    Picture = null,
                    Name = "Varkensgebraad",
                    Price = 2.50M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "0de18a96-f057-4c56-9853-6f56dfa80716",
                    Picture = null,
                    Name = "Kipsla",
                    Price = 2.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "4aaaf91c-0347-4cb2-b277-3c442d7cbbeb",
                    Picture = null,
                    Name = "Kip-curry",
                    Price = 2.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "54447735-be26-4ab5-9cbb-2e2ad0ba9e6e",
                    Picture = null,
                    Name = "Eiersla",
                    Price = 2.30M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "c6c01c33-7635-415d-afd7-81436ddfb79d",
                    Picture = null,
                    Name = "Zalm, boter, ui",
                    Price = 3.20M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "98870e31-8408-41f4-b5d5-56cfc0a085b3",
                    Picture = null,
                    Name = "Krabsla",
                    Price = 2.50M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "57bc706d-c6dc-4d6c-b4ad-5f7a0c390f40",
                    Picture = null,
                    Name = "Garnaalsla",
                    Price = 3.10M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "9861be99-15e3-4454-9949-1dba6a0b57b1",
                    Picture = null,
                    Name = "Tonijnsla",
                    Price = 2.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "05b67192-af77-4ad5-bb2d-e332af34ff61",
                    Picture = null,
                    Name = "Tonijn cocktail",
                    Price = 2.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "436fc08a-e3cf-4432-9567-b12cd1380f82",
                    Picture = null,
                    Name = "Tonijn speciaal",
                    Description = "ananas, gruyère, tabasco",
                    Price = 2.70M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "06535db2-a41e-414d-89ad-4b041914427b",
                    Picture = null,
                    Name = "Tartaal speciaal",
                    Description = "préparé, tartaar, ei",
                    Price = 2.80M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "2f4f9098-baae-4605-b392-375d15bcbc75",
                    Picture = null,
                    Name = "Speciaal",
                    Description = "préparé, ui, tomaten ketchup",
                    Price = 2.90M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "57e31d95-55b0-4808-b7d7-423955804e01",
                    Picture = null,
                    Name = "Pick-nick",
                    Description = "gebraad, cocktailsaus, sla, tomaat, augurk",
                    Price = 3.00M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "4a7daa41-5c4d-4f30-847e-f2cec55b7fa2",
                    Picture = null,
                    Name = "Mozerella",
                    Description = "basillicum, tomaat, sla, olijfolie",
                    Price = 2.90M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "ca5dc0e2-f3f3-4929-9adc-ba937171c1d0",
                    Picture = null,
                    Name = "Martino",
                    Description = "préparé, mosterd, augurk, ansjovis, tabasco, tomaat",
                    Price = 3.00M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "8fe3a520-349c-4ffc-b002-f97e3087c42e",
                    Picture = null,
                    Name = "Limburgse Martino",
                    Description = "préparé, ui, tabasco, Am. saus, ei, tomaat, sla",
                    Price = 3.00M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "b1d5fb88-267d-497e-921a-22109bf69d98",
                    Picture = null,
                    Name = "Mozarella komkommer",
                    Description = "rozijnen, walnote, nootolie, sla",
                    Price = 3.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "9b4cc9fa-7479-4285-b793-e8fc8bbe474f",
                    Picture = null,
                    Name = "Smos",
                    Description = "hesp, kaas, mmay, augurk, ei, tomaat, sla",
                    Price = 2.90M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "5d965216-7113-46d1-91dc-52f000af7b56",
                    Picture = null,
                    Name = "Smos - kaas",
                    Description = "mayo, augurk, ei, sla, tomaat",
                    Price = 2.70M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "a4d43eba-22d4-4a0f-a047-ba3770b9ab88",
                    Picture = null,
                    Name = "Smos - hesp",
                    Description = "mayo, augurk, ei, sla, tomaat",
                    Price = 2.70M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "e9e90ecc-d849-484d-8aee-aa505e33aa70",
                    Picture = null,
                    Name = "Maison",
                    Description = "ham, kaas, cocktailsaus, augurk, mosterd, sla, tomaat",
                    Price = 2.90M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "5c65c093-7518-4f44-a7ad-ed329d00b635",
                    Picture = null,
                    Name = "Club",
                    Description = "préparé, mayo, tuinkers, ei, tomaat",
                    Price = 3.00M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "3d946fe8-15f7-4c2c-a074-6453c916563c",
                    Picture = null,
                    Name = "Varié",
                    Description = "hesp, cocktilsaus, ansjovis, tomaat, augurk, sla",
                    Price = 2.90M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "8cbdc127-590a-4d42-a385-6355f44e3afe",
                    Picture = null,
                    Name = "Vegetariër",
                    Description = "seldersla, asperges, augurk, tomaat, sla",
                    Price = 2.80M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "3491bc40-47a1-4d35-a3db-9846d9f96255",
                    Picture = null,
                    Name = "Tonijntino",
                    Description = "tonijn, paprika, tabasco, mosterd, ansjovis, tomaat, augurken",
                    Price = 2.90M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "34d24f1f-4453-4b9e-a1cd-d38338231af3",
                    Picture = null,
                    Name = "Fijn lijntje",
                    Description = "philadelphia, ui, tuinkers, tomaat",
                    Price = 2.80M
                },new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "3e5cc44f-fe0d-41d0-975c-cb308103ba36",
                    Picture = null,
                    Name = "Tropical",
                    Description = "ham, kaas, cocktailsaus, ananas, sla",
                    Price = 2.90M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "9864c2fb-e763-4684-9207-db9c019dc8d3",
                    Picture = null,
                    Name = "Mozarella - serrano",
                    Description = "pesto, tuinkers, gedroogde tomaten",
                    Price = 3.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "bd7833f0-0ded-4d52-ac70-9cc37048e7ba",
                    Picture = null,
                    Name = "Mozarella - zalm",
                    Description = "tomaat, ui, olie, sla",
                    Price = 3.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "a75e865b-bc78-4add-a05c-b69f89dc4c9b",
                    Picture = null,
                    Name = "Citroen - zalm",
                    Description = "ui, citroensap, peterselie",
                    Price = 3.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "f3b2a964-3521-4208-9874-402dcd74bb38",
                    Picture = null,
                    Name = "Zalm - tartaar",
                    Description = "peterselie, tomaat, sla",
                    Price = 3.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "76cb8e80-d8f9-4bb3-8bf0-73584ccb2d25",
                    Picture = null,
                    Name = "Effi - serrano",
                    Description = "komkommer, tomaat, sla",
                    Price = 3.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "ef0de421-25eb-4d53-9cf1-c8dac17178b9",
                    Picture = null,
                    Name = "Effi - salami",
                    Description = "komkommer, tomaat, sla",
                    Price = 3.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "f6bb63dc-c63d-42e7-9f8d-8333b99e5236",
                    Picture = null,
                    Name = "Brie",
                    Description = "rozijnen, tuinkers, honing",
                    Price = 3.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "081a0aa5-eee8-449a-a24e-5eb32eeab932",
                    Picture = null,
                    Name = "Brie - pijnboompitjes",
                    Description = "rozijnen, tuinkers, honing",
                    Price = 3.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "ef215a15-11a6-4c77-b598-489c207043cc",
                    Picture = null,
                    Name = "Brie - nootolie",
                    Description = "zonnebloempitjes, tuinkers",
                    Price = 3.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "61f0bcc7-c17c-40e5-af0a-962bbb76d945",
                    Picture = null,
                    Name = "Geitenkaas",
                    Description = "komkommer, tuinkers, sap van zontomaatjes",
                    Price = 3.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "f89416be-ab9a-49fe-a9ab-d79003d41e12",
                    Picture = null,
                    Name = "Perzik - gebraad",
                    Description = "perzik, cocktailsaus",
                    Price = 3.00M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "71e14d94-fdf7-4a33-bc84-902ccc9f8534",
                    Picture = null,
                    Name = "Kip - ananas",
                    Price = 2.70M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "48532839-f6b0-4a69-b23a-5f9f85ef697b",
                    Picture = null,
                    Name = "Kip - perzik",
                    Price = 2.70M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "882e4c36-98a0-4e82-b2dd-0ef5b4c89fc4",
                    Picture = null,
                    Name = "Kippenblokjes",
                    Description = "komkommer, mayo, wortel, sla, tomaat",
                    Price = 3.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "37d2b45c-3b74-4b3a-8a23-f10a9b8e85ab",
                    Picture = null,
                    Name = "Kippewit",
                    Description = "cocktailsaus, ei, ananas, tuinkers",
                    Price = 3.00M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "8e422b83-a183-4f44-8c15-e86ec37fc915",
                    Picture = null,
                    Name = "Préparé - zilverui",
                    Description = "tomaten ketchup, ei, sla",
                    Price = 3.00M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "424b1a0a-1bd1-4637-adb1-80ad76ecca00",
                    Picture = null,
                    Name = "Eiersla speciaal",
                    Description = "spekjes, komkommer, tomaat, sla",
                    Price = 3.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "507389ef-4e8c-4996-8083-1be33a5a8a58",
                    Picture = null,
                    Name = "Boulette - kriekenconfituur",
                    Price = 2.90M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "4727626d-7172-40ba-a4e5-23b395e6067d",
                    Picture = null,
                    Name = "Boulette - tartaar",
                    Description = "ei, tuinkers, tomaat",
                    Price = 3.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "7f40b582-9ca0-448e-8461-1f2bf6ef740c",
                    Picture = null,
                    Name = "Boulette",
                    Description = "mayo, ui, curry ketchup",
                    Price = 2.90M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "6b4a12b5-5290-44bf-a810-851fcd623358",
                    Picture = null,
                    Name = "Omelet",
                    Description = "mayo, sla, tomaat",
                    Price = 3.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "82c3271c-8301-4f4c-87af-efcd51dad8d6",
                    Picture = null,
                    Name = "Omelet - spek",
                    Description = "mayo, sla, tomaat",
                    Price = 3.70M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "e86cb780-444d-4e3e-975e-13b82dbb7be2",
                    Picture = null,
                    Name = "Geitenkaas - spekjes",
                    Description = "honing, rozijnen, perzik, rucola",
                    Price = 3.80M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "45e9d72a-e546-4d2a-a74d-783f288afbc2",
                    Picture = null,
                    Name = "Brie - spekjes",
                    Description = "honing, rozijnen, perzik, rucola",
                    Price = 3.80M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "22f109c8-8a8a-402e-aa98-e245a63afc9b",
                    Picture = null,
                    Name = "Warme kip",
                    Description = "spek, dressing, kipblokjes, wortel, komkommer, sla, tomaten",
                    Price = 4.40M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "cbd5d971-de69-4f71-a13d-718bb9b00b46",
                    Picture = null,
                    Name = "Argenteuil",
                    Description = "ham, cocktailsaus, augurk, asperges, sla, tomaten",
                    Price = 2.90M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "ed02787e-d662-4a02-8468-04b075ac0334",
                    Picture = null,
                    Name = "Gezond",
                    Description = "eiersla, tuinkers, wortel, tomaat, sla",
                    Price = 2.80M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "6f39de95-4224-44c1-9127-a44d8c73192b",
                    Picture = null,
                    Name = "Mozerella speciaal",
                    Description = "pesto, zontomaat, parma, rucola, pijnboompitten",
                    Price = 4.00M
                },
                new MenuEntry
                {
                    CategoryId = categoryKoudePastaSalades1,
                    Enabled = true,
                    Id = "96d0b5be-7264-40d0-aa6a-1d105e36f875",
                    Picture = null,
                    Name = "Fusilli - kip",
                    Description = "kippeblokjes, dressing, walnoot, mandarijn, sla, wortel, tomaat, komkommer, ei",
                    Price = 4.50M
                },
                new MenuEntry
                {
                    CategoryId = categoryKoudePastaSalades1,
                    Enabled = true,
                    Id = "d31d9a09-cc55-43e8-9dce-20f66702b38e",
                    Picture = null,
                    Name = "Fusilli - vleesbrood",
                    Description = "blokjes vleesbrood, walnoot, zongedroogde tomaatjes, dressing, sla, wortel, tomaat, komkommer, ei",
                    Price = 4.50M
                },
                new MenuEntry
                {
                    CategoryId = categoryKoudePastaSalades1,
                    Enabled = true,
                    Id = "583102d6-101f-4508-8c31-2de9b8a89c5a",
                    Picture = null,
                    Name = "Fusilli - mozarella",
                    Description = "perzik, walnootjes, dressing, sla, wortel, tomaat, komkommer, ei",
                    Price = 4.50M
                },
                new MenuEntry
                {
                    CategoryId = categoryKoudePastaSalades1,
                    Enabled = true,
                    Id = "4b914694-7486-4bd8-89a3-702abe71f780",
                    Picture = null,
                    Name = "Fusilli - veggie",
                    Description = "perzik, ananas, mandarijn, sla, wortel, tomaat, komkommer, ei",
                    Price = 4.50M
                },
                new MenuEntry
                {
                    CategoryId = categoryKoudePastaSalades1,
                    Enabled = true,
                    Id = "ae335332-0e76-47e2-9ec3-da3c384b3208",
                    Picture = null,
                    Name = "Fusilli - ham kaas",
                    Description = "ham, kaas, dressing, walnoot, sla, wortel, tomaat, komkommer, ei",
                    Price = 4.50M
                },
                new MenuEntry
                {
                    CategoryId = categoryKoudePastaSalades2,
                    Enabled = true,
                    Id = "6f38c32f-adaf-4a12-9757-ed8f2877f744",
                    Picture = null,
                    Name = "Fusilli - garnaal",
                    Description = "dressing, wortel, tomaat, komkommer, walnoot, sla, ei",
                    Price = 6.00M
                },
                new MenuEntry
                {
                    CategoryId = categoryKoudePastaSalades2,
                    Enabled = true,
                    Id = "36cb500c-bafa-4270-84c9-a595ff464065",
                    Picture = null,
                    Name = "Fusilli - zalm",
                    Description = "dressing, wortel, tomaat, komkommer, walnoot, sla, ei",
                    Price = 6.00M
                },
                new MenuEntry
                {
                    CategoryId = categoryBroodjesId,
                    Enabled = true,
                    Id = "f6030434-3956-46bd-815b-491521adb61d",
                    Picture = null,
                    Name = "Fusilli - kip- en spekblokjes",
                    Description = "currydressing, warme kip en spek, gedroogde ui, sla, wortel, tomaat, komkommer, ei",
                    Price = 6.00M
                },
                new MenuEntry
                {
                    CategoryId = categoryWarmePasta1,
                    Enabled = true,
                    Id = "1f7b2b81-5ab5-43b5-89a1-57e2baf6eba3",
                    Picture = null,
                    Name = "Pasta bolognaise",
                    Price = 4.00M
                },
                new MenuEntry
                {
                    CategoryId = categoryWarmePasta1,
                    Enabled = true,
                    Id = "b0a5187d-6712-4c52-a353-aff5b1253234",
                    Picture = null,
                    Name = "Pasta met kaassaus",
                    Price = 4.00M
                },
                new MenuEntry
                {
                    CategoryId = categoryWarmePasta1,
                    Enabled = true,
                    Id = "d23fcaad-f919-48c1-95d0-6dc4d042fffb",
                    Picture = null,
                    Name = "Pasta veggie",
                    Description = "tomatensaus, philadelphia",
                    Price = 4.00M
                },
                new MenuEntry
                {
                    CategoryId = categoryWarmePasta1,
                    Enabled = true,
                    Id = "fc13b1f5-714b-4d26-960b-ac40b9decf10",
                    Picture = null,
                    Name = "Pasta ham - kaas",
                    Price = 4.00M
                },
                new MenuEntry
                {
                    CategoryId = categoryWarmePasta2,
                    Enabled = true,
                    Id = "ff4034ef-e2fa-4170-b1f7-e26cbb759f5f",
                    Picture = null,
                    Name = "Pasta veggie special",
                    Description = "mozarella, pijnboompitten",
                    Price = 4.50M
                },
                new MenuEntry
                {
                    CategoryId = categoryWarmePasta2,
                    Enabled = true,
                    Id = "4fdef78d-48ef-4ba0-8e16-1dcb1c6a6fd3",
                    Picture = null,
                    Name = "Pasta kip - tomaat",
                    Price = 4.50M
                },
                new MenuEntry
                {
                    CategoryId = categoryWarmePasta2,
                    Enabled = true,
                    Id = "d39ffd63-a93f-4380-a094-0cddfc69684b",
                    Picture = null,
                    Name = "Pasta kaassaus - spekjes",
                    Price = 4.50M
                },
                new MenuEntry
                {
                    CategoryId = categoryWarmePasta2,
                    Enabled = true,
                    Id = "512e6e00-9d0c-499e-ac0d-0015e4fec42d",
                    Picture = null,
                    Name = "Pasta kip - kaassaus",
                    Price = 4.50M
                },
                new MenuEntry
                {
                    CategoryId = categoryWarmePasta2,
                    Enabled = true,
                    Id = "eedfc981-a619-4aaa-bd0d-3d6079acf067",
                    Picture = null,
                    Name = "Pasta 4 kazen - zalm",
                    Price = 4.50M
                },
                new MenuEntry
                {
                    CategoryId = categoryCroques,
                    Enabled = true,
                    Id = "92b6f7e3-0086-4cf8-97e6-ef90f058406b",
                    Picture = null,
                    Name = "Croque uit 't vuistje",
                    Price = 2.50M
                },
                new MenuEntry
                {
                    CategoryId = categoryCroques,
                    Enabled = true,
                    Id = "5a279b20-84a0-4b35-94b4-aad62ea76cb6",
                    Picture = null,
                    Name = "Croque monsieur",
                    Price = 5.00M
                },
                new MenuEntry
                {
                    CategoryId = categoryCroques,
                    Enabled = true,
                    Id = "d9201d15-b3b2-4c74-a2f8-d8217a066989",
                    Picture = null,
                    Name = "Croque dubbel",
                    Price = 8.00M
                },
                new MenuEntry
                {
                    CategoryId = categoryCroques,
                    Enabled = true,
                    Id = "05df3532-247d-4e58-a023-81c7e4a05dbd",
                    Picture = null,
                    Name = "Croque hawaï",
                    Price = 7.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades1,
                    Enabled = true,
                    Id = "5b6dc61a-15e3-4848-b478-2095e16daf9e",
                    Picture = null,
                    Name = "Salade neptune",
                    Description = "krabsla, dressing, ei, komkommer, tuinkers, tomaat, wortel, sla",
                    Price = 5.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades1,
                    Enabled = true,
                    Id = "9bf8c623-60dd-43f4-beb9-7b60350f008d",
                    Picture = null,
                    Name = "Perzik - tonijn salade",
                    Description = "tonijn, dressing, perzik, tuinkers, ei, tomaat, komkommer, wortel, sla",
                    Price = 5.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades1,
                    Enabled = true,
                    Id = "1a9ad8a8-5122-4172-9159-371f77c4c0fc",
                    Picture = null,
                    Name = "Kaassla",
                    Description = "kaas, gemarineerde tomaatjes, pijnboompitjes, sla, tomaat, tuinkers, wortel, ei, komkommer",
                    Price = 5.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades1,
                    Enabled = true,
                    Id = "e442e4c7-50a5-43a5-b2a4-30d8195c6660",
                    Picture = null,
                    Name = "Salade argenteuille",
                    Description = "kaas, ham, asperges, dressing, wortel, tomaat, sla, tuinkers, ei",
                    Price = 5.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades1,
                    Enabled = true,
                    Id = "f5e1511d-b8c7-4065-b24b-c8f9693ff075",
                    Picture = null,
                    Name = "Kaas - ham salade",
                    Description = "kaas, hesp, dressing, tuinkers, ei, tomaat, wortel, komkommer, sla",
                    Price = 5.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades1,
                    Enabled = true,
                    Id = "35db4e3a-e88c-4189-a0d4-de41dc669c7b",
                    Picture = null,
                    Name = "Warme bouletjes salade",
                    Description = "bouletjes, dressing, tuinkers, ei, tomaat, komkommer, sla",
                    Price = 5.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades1,
                    Enabled = true,
                    Id = "55e3e515-4250-4e0d-b2b9-11f5004eb59d",
                    Picture = null,
                    Name = "Préparé salade",
                    Description = "préparé, dressing, tuinkers, ei, tomaat, komkommer, sla",
                    Price = 5.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades1,
                    Enabled = true,
                    Id = "1dadc40a-06b2-4fd6-b8f4-1ac83376f622",
                    Picture = null,
                    Name = "Mozarella salade",
                    Description = "mozarella, dressing, zontomaatjes, nootjes, rozijnen, tuinkers, ei, sla, wortel, komkommer, tomaat",
                    Price = 5.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades1,
                    Enabled = true,
                    Id = "82858946-42f5-4e22-b3f6-9309d52c97f6",
                    Picture = null,
                    Name = "Veggie salade",
                    Description = "zontomaatjes, nootjes, rozijntjes, tuinkers, sla, ei, wortel, tomaat, komkommer",
                    Price = 5.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades2,
                    Enabled = true,
                    Id = "26baf467-06fe-42b9-8691-574b5af2cc99",
                    Picture = null,
                    Name = "Salade niçoise",
                    Description = "tonijn, ansjovis, paprika, olijven, ui, ei, wortel, tomaat, komkommer, sla, tuinkers",
                    Price = 6.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades2,
                    Enabled = true,
                    Id = "8d96b4b2-7fab-4191-b4e5-5a48bc487780",
                    Picture = null,
                    Name = "Mandarijn - kip salade",
                    Description = "kip, mandarijn, dressing, tuinkers, sla, ei, wortel, komkommer, tomaat",
                    Price = 6.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades2,
                    Enabled = true,
                    Id = "2ebf9ed0-a14d-4103-96d2-deaf58428bc1",
                    Picture = null,
                    Name = "Kip - spek salade",
                    Description = "dressing, kip, spek, tuinkers, sla, ei, tomaat, komkommer, wortel",
                    Price = 6.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades2,
                    Enabled = true,
                    Id = "46350547-6435-44f8-9dca-d4a0dace4e9b",
                    Picture = null,
                    Name = "Salade Swiss",
                    Description = "hesp, dressing, emmentalkaas, walnoten, tomaat, tuinkers, ei, sla, komkommer",
                    Price = 6.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades2,
                    Enabled = true,
                    Id = "e37e4a92-ea99-46f2-8201-24a3c215f744",
                    Picture = null,
                    Name = "Salade lardons",
                    Description = "spekjes, dressing, tuinkers, ei, wortel, tomaat, komkommer, sla",
                    Price = 6.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades2,
                    Enabled = true,
                    Id = "b102bf05-f33c-47a8-828c-92ae8a013c6a",
                    Picture = null,
                    Name = "Gerookte zalm salade",
                    Description = "gerookte zalm, sla, tuinkers, wortel, tomaat, komkommer, ei",
                    Price = 6.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades2,
                    Enabled = true,
                    Id = "37a5434a-720b-4169-a7e7-afe3c44b3c8a",
                    Picture = null,
                    Name = "Tomaat - garnaal salade",
                    Description = "sla, tuinkers, ei, komkommer, tomaat, wortel, garnaal",
                    Price = 6.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades2,
                    Enabled = true,
                    Id = "9ba425e4-24b6-4aa8-8829-6686b3475697",
                    Picture = null,
                    Name = "Griekse salade",
                    Description = "paprika, olijven, komkommer, kaasblokjes, dressing, ui, tuinkers, sla, tomaat, wortel, ei",
                    Price = 6.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades3,
                    Enabled = true,
                    Id = "a1fd430c-fdf9-4a7f-b753-36c22048c398",
                    Picture = null,
                    Name = "Geitenkaas salade",
                    Description = "gebakken geitenkaasjes, spek, dressing, ananas, perzik, rozijn, sla, tuinkers, wortel, komkommer, ei, tomaat",
                    Price = 8.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades3,
                    Enabled = true,
                    Id = "d06d13c6-f655-4a4d-a909-cf17a5de383d",
                    Picture = null,
                    Name = "Warme salade",
                    Description = "Warme kip- en spekblokjes, currydressing, gedroogde ui, sla, tuinkers, wortel, ei, tomaat, sla",
                    Price = 8.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades3,
                    Enabled = true,
                    Id = "b3a50568-4094-4bb6-9e92-b0cdbb4bea5d",
                    Picture = null,
                    Name = "Serrano - meloen",
                    Description = "dressing, pijnboompitjes, zontomaatjes, rucola, tomaat, wortel, ei, sla, komkommer",
                    Price = 8.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySalades3,
                    Enabled = true,
                    Id = "59e527db-4052-4e70-a30a-237eb88858af",
                    Picture = null,
                    Name = "Vis salade",
                    Description = "tuinkers, tomaat, wortel, ei, sla, komkommer, zalm, heilbot, garnalen",
                    Price = 8.00M
                },
                new MenuEntry
                {
                    CategoryId = categorySoep,
                    Enabled = true,
                    Id = "4f5226e6-a54e-41d2-a3c8-513a5164cc48",
                    Picture = null,
                    Name = "Soep",
                    Price = 2.50M
                },
                new MenuEntry
                {
                    CategoryId = categorySoep,
                    Enabled = true,
                    Id = "205c4bef-f647-4e4d-9609-3a32b076a50e",
                    Picture = null,
                    Name = "Soep + broodje",
                    Price = 3.00M
                }
            };

            var menuRules = new List<MenuRule>
            {
                new MenuRule
                {
                    Id = "2af8c647-7a8e-4f8e-b4a2-5417686e0267",
                    Enabled = true,
                    Description = "Bruin brood",
                    PriceDelta = 0M,
                    CategoryIds = new List<string> {categoryBroodjesId}
                },
                new MenuRule
                {
                    Id = "f8330726-8155-49cf-9476-62dac25281c4",
                    Enabled = true,
                    Description = "Supplement Groenten (tomaat, sla, ei, wortel)",
                    PriceDelta = 0.50M,
                    CategoryIds = new List<string> {categoryBroodjesId}
                },
                new MenuRule
                {
                    Id = "6621339a-7602-4e66-b8b0-922e3d37462a",
                    Enabled = true,
                    Description = "Groot",
                    PriceDelta = 1.00M,
                    CategoryIds = new List<string> {categoryKoudePastaSalades1, categorySalades1, categorySalades2, categorySalades3}
                },
                new MenuRule
                {
                    Id = "09f462a5-8463-483d-b75a-a5ad69a38730",
                    Enabled = true,
                    Description = "Groot",
                    PriceDelta = 1.50M,
                    CategoryIds = new List<string> {categoryKoudePastaSalades2, categoryWarmePasta1, categoryWarmePasta2}
                }
            };

            var menu = new Menu
            {
                Id = Guid.NewGuid(),
                Enabled = false,
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
            var userInfo = await DatabaseRepository.GetUserInfo(TestConstants.User1.UserName);
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
            Assert.AreEqual(TestConstants.User1.FirstName, userInfo.Profile.FirstName);
            Assert.AreEqual(TestConstants.User1.LastName, userInfo.Profile.LastName);
            Assert.AreEqual(TestConstants.User1.Profile.Culture, userInfo.Profile.Culture);
            Assert.AreEqual(TestConstants.User1.Profile.Picture, userInfo.Profile.Picture);
        }
    }
}
