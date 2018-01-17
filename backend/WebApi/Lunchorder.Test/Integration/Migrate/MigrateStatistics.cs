using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lunchorder.Domain.Entities.DocumentDb;
using Lunchorder.Test.Integration.Helpers;
using Lunchorder.Test.Integration.Helpers.Base;
using NUnit.Framework;

namespace Lunchorder.Test.Integration.Migrate
{
    [TestFixture]
    public class MigrateStatistics : RepositoryBase
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
            Guid.Parse("eedfc981-a619-4aaa-bd0d-3d6079acf067")
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
            Guid.Parse("59e527db-4052-4e70-a30a-237eb88858af")
        };

        [Test]
        public async Task CalculateStatistics()
        {
            var userId = "";
            // get a user
            var applicationUser = await DatabaseRepository.GetApplicationUser(TestConstants.User1.UserName);
            var userOrderHistories = DocumentDbBase.DocumentStore.GetItems<UserOrderHistory>(x => x.UserId == userId);

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
                }


                // calculate badges

                // remove historical data

                // save document

            }
        }
    }
}
