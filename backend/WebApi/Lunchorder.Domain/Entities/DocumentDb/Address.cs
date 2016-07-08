namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// Represents an addresss
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Address street without number
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Address street number
        /// </summary>
        public string StreetNumber { get; set; }

        /// <summary>
        /// Address city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Phone or cellphone that is used to reach the address
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The email that is related to the address
        /// </summary>
        public string Email { get; set; }
    }
}