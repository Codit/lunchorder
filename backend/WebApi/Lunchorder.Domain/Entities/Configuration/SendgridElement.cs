using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    public class SendgridElement : ConfigurationElement
    {
        [ConfigurationProperty("apiKey", IsRequired = true)]
        public string ApiKey
        {
            get
            {
                return base["apiKey"].ToString();
            }
            set
            {
                base["apiKey"] = value;
            }
        }

        [ConfigurationProperty("from", IsRequired = true)]
        public string From
        {
            get
            {
                return base["from"].ToString();
            }
            set
            {
                base["from"] = value;
            }
        }
    }
}