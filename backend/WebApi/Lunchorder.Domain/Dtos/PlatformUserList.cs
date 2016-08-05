using System.Collections.Generic;

namespace Lunchorder.Domain.Dtos
{
    public class PlatformUserList
    {
        public string Id { get; set; }
        public IEnumerable<PlatformUserListItem> Users { get; set; }
    }
}