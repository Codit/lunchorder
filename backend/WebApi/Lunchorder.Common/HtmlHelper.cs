using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Lunchorder.Domain.Dtos;

namespace Lunchorder.Common
{
    public class HtmlHelper
    {
        public static string CreateVendorHistory(VendorOrderHistory vendorOrderHistory)
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
                    }

                    th 
                    {
                        background-color: #c6d2ff;
                    }
                    tr:nth-child(even)
                    {
                        background-color: #f2f2f2;
                    }
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
                    HtmlGenericControl tableHeadTitleUser = new HtmlGenericControl("th") { InnerText = "Username"};
                    HtmlGenericControl tableHeadTitleOrder = new HtmlGenericControl("th") { InnerText = "Order" };
                    HtmlGenericControl tableHeadTitlePrice = new HtmlGenericControl("th") { InnerText = "Price" };

                    tableHeadControl.Controls.Add(tableHeadTitleUser);
                    tableHeadControl.Controls.Add(tableHeadTitleOrder);
                    tableHeadControl.Controls.Add(tableHeadTitlePrice);
                    tableControl.Controls.Add(tableHeadControl);

                    foreach (var entry in vendorOrderHistory.Entries)
                    {
                        var tableRow = new HtmlGenericControl("tr");
                        var tableColumnUsername = new HtmlGenericControl("td") { InnerText = entry.UserName };
                        

                        var tableColumnOrder = new HtmlGenericControl("td") { InnerText = entry.Name };

                        // todo add rules
                        var tableColumnPrice = new HtmlGenericControl("td")
                        {
                            InnerText = entry.FinalPrice.ToString(CultureInfo.InvariantCulture)
                        };

                        tableRow.Controls.Add(tableColumnUsername);
                        tableRow.Controls.Add(tableColumnOrder);
                        tableRow.Controls.Add(tableColumnPrice);
                        tableControl.Controls.Add(tableRow);
                    }

                    
                    divControl.Controls.Add(tableControl);
                    html.Controls.Add(head);
                    html.Controls.Add(body);
                    html.RenderControl(htmlWriter);
                }
                return stringWriter.ToString();
            }
        }
    }
}