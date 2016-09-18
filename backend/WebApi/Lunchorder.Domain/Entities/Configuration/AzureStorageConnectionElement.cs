using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    public class AzureStorageConnectionElement : ConfigurationElement
    {
        [ConfigurationProperty("connectionString", IsRequired = true)]
        public string ConnectionString
        {
            get
            {
                return base["connectionString"] as string;
            }
            set
            {
                base["connectionString"] = value;
            }
        }

        [ConfigurationProperty("imageContainerName", IsRequired = true)]
        public string ImageContainerName
        {
            get
            {
                return base["imageContainerName"] as string;
            }
            set
            {
                base["imageContainerName"] = value;
            }
        }
    }
}