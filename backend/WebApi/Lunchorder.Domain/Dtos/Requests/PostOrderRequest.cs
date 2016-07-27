using System;
using System.Collections.Generic;

namespace Lunchorder.Domain.Dtos.Requests
{
    public class PostOrderRequest
    {
        public IEnumerable<MenuOrder> MenuOrders { get; set; }
    }
}