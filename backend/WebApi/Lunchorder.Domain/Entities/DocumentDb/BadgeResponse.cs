using System;
using System.Collections.Generic;
using Lunchorder.Domain.Constants;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// Represents a badge that can be earned by a user
    /// </summary>
    public class BadgeResponse
    {
        /// <summary>
        /// The id of the badge
        /// </summary>
        public Guid Id { get; set; }

        public IEnumerable<Badge> Badges { get; set; }

        public string Type = DocumentDbTypes.Badges;
    }
}