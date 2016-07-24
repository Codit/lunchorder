namespace Lunchorder.Domain.Dtos
{
    /// <summary>
    /// Represents the profile of the user
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// First name of the user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Picture of the user (url)
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// The UI culture of the user
        /// </summary>
        public string Culture { get; set; }
    }
}
