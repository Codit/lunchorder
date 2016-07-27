using System;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// Represents a menu entry favorite for a user
    /// </summary>
    public class MenuEntryFavorite
    {
        /// <summary>
        /// Associates a favorite with a menu entry id
        /// </summary>
        public Guid MenuEntryId { get; set; }
    }
}