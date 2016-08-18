using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Lunchorder.Domain.Entities.Configuration
{
    /// <summary>
    /// Collection of Jobs
    /// </summary>
    [ConfigurationCollection(typeof(JobsElementCollection), AddItemName = "job")]
    public class JobsElementCollection : ConfigurationElementCollection, IEnumerable<JobElement>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new JobElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var jobElement = element as JobElement;
            return jobElement != null ? jobElement.Name : null;
        }

        public JobElement this[int index]
        {
            get
            {
                return BaseGet(index) as JobElement;
            }
        }

        #region IEnumerable<ConfigElement> Members

        IEnumerator<JobElement> IEnumerable<JobElement>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, Count)
                select this[i])
                .GetEnumerator();
        }

        #endregion
    }
}