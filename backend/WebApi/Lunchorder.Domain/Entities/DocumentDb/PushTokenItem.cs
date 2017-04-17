using System;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    public class PushTokenItem
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime LastModified { get; set; }
    }
}