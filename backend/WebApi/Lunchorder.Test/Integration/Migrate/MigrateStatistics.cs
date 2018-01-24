using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lunchorder.Domain.Constants;
using Lunchorder.Domain.Entities.Authentication;
using Lunchorder.Domain.Entities.DocumentDb;
using Lunchorder.Test.Integration.Helpers.Base;
using NUnit.Framework;

namespace Lunchorder.Test.Integration.Migrate
{
    [TestFixture]
    public class MigrateStatistics : RepositoryBaseSeededDb
    {
        private List<Guid> _pastaEntriesList = new List<Guid>
        {
            Guid.Parse("96d0b5be-7264-40d0-aa6a-1d105e36f875"),
            Guid.Parse("d31d9a09-cc55-43e8-9dce-20f66702b38e"),
            Guid.Parse("583102d6-101f-4508-8c31-2de9b8a89c5a"),
            Guid.Parse("4b914694-7486-4bd8-89a3-702abe71f780"),
            Guid.Parse("ae335332-0e76-47e2-9ec3-da3c384b3208"),
            Guid.Parse("6f38c32f-adaf-4a12-9757-ed8f2877f744"),
            Guid.Parse("36cb500c-bafa-4270-84c9-a595ff464065"),
            Guid.Parse("f6030434-3956-46bd-815b-491521adb61d"),
            Guid.Parse("1f7b2b81-5ab5-43b5-89a1-57e2baf6eba3"),
            Guid.Parse("b0a5187d-6712-4c52-a353-aff5b1253234"),
            Guid.Parse("d23fcaad-f919-48c1-95d0-6dc4d042fffb"),
            Guid.Parse("fc13b1f5-714b-4d26-960b-ac40b9decf10"),
            Guid.Parse("ff4034ef-e2fa-4170-b1f7-e26cbb759f5f"),
            Guid.Parse("4fdef78d-48ef-4ba0-8e16-1dcb1c6a6fd3"),
            Guid.Parse("d39ffd63-a93f-4380-a094-0cddfc69684b"),
            Guid.Parse("512e6e00-9d0c-499e-ac0d-0015e4fec42d"),
            Guid.Parse("eedfc981-a619-4aaa-bd0d-3d6079acf067"),
            Guid.Parse("8ff26df1-52d2-4e91-b173-f3b4243b0582"),
            Guid.Parse("63ccb5ed-c473-46a3-979c-729e228f1369"),
            Guid.Parse("79ac6579-1597-4d22-8b7f-cc7706b1634f"),
            Guid.Parse("5bb05950-a654-4000-9373-e9898546a75b"),
            Guid.Parse("13bd5070-8e8c-4884-8852-22580f9f99a0"),
            Guid.Parse("3c9dc98f-8c59-49b7-8e2e-9fa33c12f7d5"),
            Guid.Parse("57b13de1-6e3b-4d9a-b8be-c225017c7e65")
        };

        private List<Guid> _healtyEntriesList = new List<Guid>
        {
            Guid.Parse("b1d5fb88-267d-497e-921a-22109bf69d98"),
            Guid.Parse("8cbdc127-590a-4d42-a385-6355f44e3afe"),
            Guid.Parse("a75e865b-bc78-4add-a05c-b69f89dc4c9b"),
            Guid.Parse("ed02787e-d662-4a02-8468-04b075ac0334"),
            Guid.Parse("5b6dc61a-15e3-4848-b478-2095e16daf9e"),
            Guid.Parse("9bf8c623-60dd-43f4-beb9-7b60350f008d"),
            Guid.Parse("1a9ad8a8-5122-4172-9159-371f77c4c0fc"),
            Guid.Parse("e442e4c7-50a5-43a5-b2a4-30d8195c6660"),
            Guid.Parse("f5e1511d-b8c7-4065-b24b-c8f9693ff075"),
            Guid.Parse("1dadc40a-06b2-4fd6-b8f4-1ac83376f622"),
            Guid.Parse("82858946-42f5-4e22-b3f6-9309d52c97f6"),
            Guid.Parse("26baf467-06fe-42b9-8691-574b5af2cc99"),
            Guid.Parse("8d96b4b2-7fab-4191-b4e5-5a48bc487780"),
            Guid.Parse("46350547-6435-44f8-9dca-d4a0dace4e9b"),
            Guid.Parse("e37e4a92-ea99-46f2-8201-24a3c215f744"),
            Guid.Parse("b102bf05-f33c-47a8-828c-92ae8a013c6a"),
            Guid.Parse("9ba425e4-24b6-4aa8-8829-6686b3475697"),
            Guid.Parse("37a5434a-720b-4169-a7e7-afe3c44b3c8a"),
            Guid.Parse("59e527db-4052-4e70-a30a-237eb88858af"),
            Guid.Parse("06a800c2-fa3e-4f22-9234-21a646f4babb"),
            Guid.Parse("722af728-51e2-48bb-8844-d71e0b9fe666"),
            Guid.Parse("92f784d3-1fa9-4392-b7b2-412101b0ce44"),
            Guid.Parse("0705f74f-6866-4944-a97e-29f8d420afe7"),
            Guid.Parse("84c39088-584a-4e1d-84d4-4495e1a1b919"),
            Guid.Parse("a8450aab-74b4-48dd-b068-60487d7f6123"),
            Guid.Parse("ce92a92e-b8b6-4183-af8b-83b83ee6dd2e"),
            Guid.Parse("ee83660d-7083-4c67-a497-eedb35be3a26"),
            Guid.Parse("d3a54d7c-cda6-4280-ae0d-78ef1d71b2b4"),
            Guid.Parse("6b0ec0b4-38d4-44d6-a6cd-e453fd979cfe"),
            Guid.Parse("9fcc421b-eb34-4a4c-a447-aa7e47d6c419"),
            Guid.Parse("d0464a9b-b07d-4424-920d-fb03a44081de"),
            Guid.Parse("cd7e933b-7718-4183-b2ec-060a02a36a08"),
            Guid.Parse("2daa2122-5a41-4b3f-8b45-166484f29ba0"),
            Guid.Parse("304954f8-7ac1-432a-9f89-e1ddf24748b5"),
            Guid.Parse("0268b20d-9ddd-432b-b5d1-d56844ad78b1"),
            Guid.Parse("42627431-5948-4457-805d-547c70a039f2"),
            Guid.Parse("d0c2dfb4-8e0b-4197-bd46-0314f541c705")
        };  

        [Test]
        public async Task CalculateStatistics()
        {
            // todo loop all users.
            var allUsersQuery = DocumentDbBase.DocumentStore.GetItems<ApplicationUser>(x => x.Type == DocumentDbType.User);
            var allUsers = allUsersQuery.ToList();
           

            foreach (var applicationUser in allUsers)
            {
                //start with a clean slate for each user
                applicationUser.Statistics = new Statistics();

                var userOrderHistoriesQuery = DocumentDbBase.DocumentStore.GetItems<UserOrderHistory>(x => x.UserId == applicationUser.UserId && x.Type == DocumentDbType.UserHistory);
                var userOrderHistories = userOrderHistoriesQuery.ToList();
                
                foreach (var userOrderHistory in userOrderHistories)
                {
                    var monthlyTotal = applicationUser.Statistics.MonthlyTotals.FirstOrDefault(x => x.MonthDate == x.ParseMonth(userOrderHistory.OrderTime));
                    if (monthlyTotal == null)
                    {
                        monthlyTotal = new MonthlyTotal(userOrderHistory.OrderTime);
                        applicationUser.Statistics.MonthlyTotals.Add(monthlyTotal);
                    }

                    var yearlyTotal = applicationUser.Statistics.YearlyTotals.FirstOrDefault(x => x.YearDate == x.ParseYear(userOrderHistory.OrderTime));
                    if (yearlyTotal == null)
                    {
                        yearlyTotal = new YearlyTotal(userOrderHistory.OrderTime);
                        applicationUser.Statistics.YearlyTotals.Add(yearlyTotal);
                    }

                    var weeklyTotal = applicationUser.Statistics.WeeklyTotals.FirstOrDefault(x => x.WeekDate == x.ParseWeek(userOrderHistory.OrderTime));
                    if (weeklyTotal == null)
                    {
                        weeklyTotal = new WeeklyTotal(userOrderHistory.OrderTime);
                        applicationUser.Statistics.WeeklyTotals.Add(weeklyTotal);
                    }

                    var dailyTotal = applicationUser.Statistics.DayTotals.FirstOrDefault(x => x.DayDate == x.ParseDay(userOrderHistory.OrderTime));
                    if (dailyTotal == null)
                    {
                        dailyTotal = new DayTotal(userOrderHistory.OrderTime);
                        applicationUser.Statistics.DayTotals.Add(dailyTotal);
                    }

                    foreach (var entry in userOrderHistory.Entries)
                    {
                        if (_pastaEntriesList.Contains(entry.Id))
                        {
                            yearlyTotal.PastaOrderCount += 1;
                        }

                        if (_healtyEntriesList.Contains(entry.Id))
                        {
                            weeklyTotal.HealthyOrderCount += 1;
                        }


                        yearlyTotal.OrderCount += 1;
                        yearlyTotal.Amount += entry.FinalPrice;
                        weeklyTotal.OrderCount += 1;
                        weeklyTotal.Amount += entry.FinalPrice;
                        monthlyTotal.OrderCount += 1;
                        monthlyTotal.Amount += entry.FinalPrice;
                        applicationUser.Statistics.AppTotalSpend += entry.FinalPrice;
                        dailyTotal.Amount += entry.FinalPrice;
                        dailyTotal.OrderCount += 1;

                        // calculate badges
                        BadgeService.ExtractOrderBadges(applicationUser, userOrderHistory, DateTime.Parse("01/01/2001 10:00:00"));
                    }
                }

                var userPrepaysQuery = DocumentDbBase.DocumentStore.GetItems<UserBalanceAudit>(x => x.UserId == applicationUser.UserId && x.Type == DocumentDbType.UserBalanceAudit);
                var userPrepays = userPrepaysQuery.ToList();

                foreach (var prepay in userPrepays)
                {
                    foreach (var audit in prepay.Audits)
                    {
                        applicationUser.Statistics.PrepayedTotal += audit.Amount;
                    }
                }

                // remove historical data
                var daysToDelete = applicationUser.Statistics.DayTotals.Where(x => x.DayDate != x.ParseDay(DateTime.UtcNow)).ToList();
                foreach (var dayToDelete in daysToDelete) { applicationUser.Statistics.DayTotals.Remove(dayToDelete); }

                var weeksToDelete = applicationUser.Statistics.WeeklyTotals.Where(x => x.WeekDate != x.ParseWeek(DateTime.UtcNow)).ToList();
                foreach (var weekToDelete in weeksToDelete) { applicationUser.Statistics.WeeklyTotals.Remove(weekToDelete); }

                var monthsToDelete = applicationUser.Statistics.MonthlyTotals.Where(x => x.MonthDate != x.ParseMonth(DateTime.UtcNow)).ToList();
                foreach (var monthToDelete in monthsToDelete) { applicationUser.Statistics.MonthlyTotals.Remove(monthToDelete); }

                var yearsToDelete = applicationUser.Statistics.YearlyTotals.Where(x => x.YearDate != x.ParseYear(DateTime.UtcNow)).ToList();
                foreach (var yearToDelete in yearsToDelete) { applicationUser.Statistics.YearlyTotals.Remove(yearToDelete); }

                // save document
                await DocumentDbBase.DocumentStore.UpsertDocument(applicationUser);
            }
        }
    }
}
