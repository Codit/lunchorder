using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos;

namespace Lunchorder.Common
{
    public class HtmlHelper
    {
        public static string CreateVendorHistory(IConfigurationService configurationService, VendorOrderHistory vendorOrderHistory)
        {
            using (var stringWriter = new StringWriter())
            {
                using (HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter))
                {
                    var html = new HtmlGenericControl("html");

                    var head = new HtmlGenericControl("head");
                    LiteralControl ltr = new LiteralControl
                    {
                        Text = "<style type=\"text/css\" rel=\"stylesheet\">" +
                               @"
                    table {
                        border-collapse: collapse;
                        width: 100%;
                        border: 1px solid #cacaca;
                    }

                    th, td
                    {
                        text-align: left;
                        padding: 10px;
                        border:1px solid black;
                    }

                    span {
                        font-size: 14px;
                        display: block;
                        padding: 5px;
                    }

                    th 
                    {
                        background-color: #c6d2ff;
                    }
                    tr:nth-child(even)
                    {
                        background-color: #f2f2f2;
                    }
                    footer { margin-top: 40px; }
					footer p.leading { font-size: 24px; font-weight: bold; }
	
                    </style>
                    "
                    };

                    head.Controls.Add(ltr);

                    var body = new HtmlGenericControl("body");

                    var heading = new HtmlGenericControl("h1")
                    {
                        InnerText = $"Order {vendorOrderHistory.OrderDate}"
                    };

                    body.Controls.Add(heading);

                    //Generate container div control  
                    HtmlGenericControl divControl = new HtmlGenericControl("div");
                    body.Controls.Add(divControl);

                    HtmlGenericControl tableControl = new HtmlGenericControl("table");

                    HtmlGenericControl tableHeadControl = new HtmlGenericControl("thead");
                    HtmlGenericControl tableHeadTitleUser = new HtmlGenericControl("th") { InnerText = "User" };
                    HtmlGenericControl tableHeadTitleOrder = new HtmlGenericControl("th") { InnerText = "Order" };
                    HtmlGenericControl tableHeadTitlePrice = new HtmlGenericControl("th") { InnerText = "Price" };

                    tableHeadControl.Controls.Add(tableHeadTitleUser);
                    tableHeadControl.Controls.Add(tableHeadTitleOrder);
                    tableHeadControl.Controls.Add(tableHeadTitlePrice);
                    tableControl.Controls.Add(tableHeadControl);

                    var totalPrice = 0M;
                    foreach (var entry in vendorOrderHistory.Entries)
                    {
                        var tableRow = new HtmlGenericControl("tr");
                        var tableColumnUsername = new HtmlGenericControl("td") { InnerText = entry.FullName };

                        var entryText = entry.Name;
                        if (!string.IsNullOrEmpty(entry.FreeText))
                        {
                            entryText = $"{entry.Name} ({entry.FreeText})";
                        }

                        var tableColumnOrder = new HtmlGenericControl("td") { InnerText = entryText };

                        if (entry.Rules != null)
                        {
                            foreach (var rule in entry.Rules)
                            {
                                var tableColumnOrderRule = new HtmlGenericControl("p") { InnerText = rule.Description };
                                tableColumnOrder.Controls.Add(tableColumnOrderRule);
                            }
                        }

                        var tableColumnPrice = new HtmlGenericControl("td")
                        {
                            InnerText = entry.FinalPrice.ToString(CultureInfo.InvariantCulture)
                        };

                        tableRow.Controls.Add(tableColumnUsername);
                        tableRow.Controls.Add(tableColumnOrder);
                        tableRow.Controls.Add(tableColumnPrice);
                        tableControl.Controls.Add(tableRow);

                        totalPrice += entry.FinalPrice;
                    }

                    var totalRow = new HtmlGenericControl("tr");
                    var emptyColumn1 = new HtmlGenericControl("td") { InnerText = "" };
                    var emptyColumn2 = new HtmlGenericControl("td") { InnerText = "" };
                    totalRow.Controls.Add(emptyColumn1);
                    totalRow.Controls.Add(emptyColumn2);
                    var totalColumn = new HtmlGenericControl("td") { InnerText = $"Total: {totalPrice}" };
                    totalRow.Controls.Add(totalColumn);
                    tableControl.Controls.Add(totalRow);

                    divControl.Controls.Add(tableControl);

                    body.Controls.Add(CreateFooter(configurationService));
                    html.Controls.Add(head);
                    html.Controls.Add(body);
                    html.RenderControl(htmlWriter);
                }
                return stringWriter.ToString();
            }
        }

        public static HtmlGenericControl CreateFooter(IConfigurationService configurationService)
        {
            HtmlGenericControl footerControl = new HtmlGenericControl("footer");
            HtmlGenericControl footerName = new HtmlGenericControl("p");
            footerName.Attributes.Add("class", "leading");
            footerName.InnerText = configurationService.Company.Name;
            footerControl.Controls.Add(footerName);
            HtmlGenericControl footerAddress = new HtmlGenericControl("p")
            {
                InnerText = configurationService.Company.Address.ToString()
            };
            footerControl.Controls.Add(footerAddress);
            HtmlGenericControl footerPhone = new HtmlGenericControl("p")
            {
                InnerText = configurationService.Company.Phone
            };
            footerControl.Controls.Add(footerPhone);
            return footerControl;
        }
    }
}