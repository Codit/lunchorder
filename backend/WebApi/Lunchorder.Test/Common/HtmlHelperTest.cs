using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lunchorder.Common;
using Lunchorder.Domain.Dtos;
using NUnit.Framework;

namespace Lunchorder.Test.Common
{
    [TestFixture]
    public class HtmlHelperTest
    {
        [Test]
        public void GenerateHtml()
        {
            var vendorOrderHistoryEntries = new List<VendorOrderHistoryEntry>()
            {
                new VendorOrderHistoryEntry
                {
                    UserName = "Jef",
                    Name = "Some item",
                    FinalPrice = 2.00M
                },
                new VendorOrderHistoryEntry
                {
                    UserName = "Sjaakie",
                    Name = "Another item",
                    FinalPrice = 3.10M
                }
            };
            var vendorOrderHistory = new VendorOrderHistory { Entries = vendorOrderHistoryEntries };
            
            vendorOrderHistory.Entries = vendorOrderHistoryEntries;
            var result  = HtmlHelper.CreateVendorHistory(vendorOrderHistory);
            Assert.IsNotNullOrEmpty(result);
        }
    }
}
