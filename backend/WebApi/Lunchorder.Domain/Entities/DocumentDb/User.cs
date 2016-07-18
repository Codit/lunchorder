using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// represent information about a user
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identifier for the user
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }
        /// <summary>
        /// The first name of the user
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The last name of the user
        /// </summary>
        public string LastName { get; set; }
    }
}
