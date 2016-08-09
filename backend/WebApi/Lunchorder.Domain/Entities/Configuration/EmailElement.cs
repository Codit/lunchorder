using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    /// <summary>
    /// Email element in the custom lunchorder configuration element
    /// </summary>
    public class EmailElement : ConfigurationElement
    {
        /// <summary>
        /// Microsoft Azure Servicebus
        /// </summary>
        [ConfigurationProperty("sendgrid")]
        public SendgridElement Sendgrid
        {
            get { return base["sendgrid"] as SendgridElement; }
        }
    }
}