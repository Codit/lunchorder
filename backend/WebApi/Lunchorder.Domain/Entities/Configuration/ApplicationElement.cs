using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    /// <summary>
    /// Application element in the custom lunchorder configuration element
    /// </summary>
    public class ApplicationElement : ConfigurationElement
    {
        /// <summary>
        /// Company element
        /// </summary>
        [ConfigurationProperty("company")]
        public CompanyElement Company
        {
            get { return base["company"] as CompanyElement; }
        }
    }
}