using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    /// <summary>
    /// Authentication element in the custom lunchorder configuration element
    /// </summary>
    public class ConnectionsElement : ConfigurationElement
    {
        /// <summary>
        /// The connection to document db
        /// </summary>
        [ConfigurationProperty("documentDb")]
        public DocumentDbConnectionElement DocumentDb
        {
            get { return base["documentDb"] as DocumentDbConnectionElement; }
        }

        /// <summary>
        /// The connection to azure storage
        /// </summary>
        [ConfigurationProperty("azureStorage")]
        public AzureStorageConnectionElement AzureStorage
        {
            get { return base["azureStorage"] as AzureStorageConnectionElement; }
        }
    }
}