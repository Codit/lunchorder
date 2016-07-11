namespace Lunchorder.Domain.Dtos
{
    /// <summary>
    /// Represents a badge that can be earned by a user
    /// </summary>
    public class Badge
    {
        /// <summary>
        /// The name of the badge
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Specific icon to represent a badge
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Description of the badge
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Determines if the badge has been earned
        /// </summary>
        public bool Earned { get; set; }
    }
}