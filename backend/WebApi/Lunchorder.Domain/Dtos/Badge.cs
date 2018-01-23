using System;

namespace Lunchorder.Domain.Dtos
{
    /// <summary>
    /// Represents a badge that can be earned by a user
    /// </summary>
    public class Badge
    {
        public Badge(Guid id, string name, bool canEarnMultipleTimes, string description, string thumbnail, string image)
        {
            Id = id;
            Name = name;
            CanEarnMultipleTimes = canEarnMultipleTimes;
            Description = description;
            Thumbnail = thumbnail;
            Image = image;
        }

        /// <summary>
        /// The id of the badge
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the badge
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Can the badge be earned multiple times?
        /// </summary>
        public bool CanEarnMultipleTimes { get; set; }

        /// <summary>
        /// The description of the badge
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The link to the badge thumbnail image
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// The link to the badge image
        /// </summary>
        public string Image { get; set; }
    }
}