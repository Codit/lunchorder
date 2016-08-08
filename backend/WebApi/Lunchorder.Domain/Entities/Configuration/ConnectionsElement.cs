using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    /// <summary>
    /// Authentication element in the custom lunchorder configuration element
    /// </summary>
    public class ConnectionsElement : ConfigurationElement
    {
        /// <summary>
        /// The collection of ApiKeys
        /// </summary>
        [ConfigurationProperty("documentDb")]
        public DocumentDbConnectionElement DocumentDb
        {
            get { return base["documentDb"] as DocumentDbConnectionElement; }
        }

        /// <summary>
        /// The collection of ApiKeys
        /// </summary>
        [ConfigurationProperty("documentDbAuth")]
        public DocumentDbConnectionElement DocumentDbAuth
        {
            get { return base["documentDbAuth"] as DocumentDbConnectionElement; }
        }
    }
}