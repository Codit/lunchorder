using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Lunchorder.Domain.Dtos.Responses
{
    public class GetUserInfoResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public decimal Balance { get; set; }
        public UserProfile Profile { get; set; }
        public IEnumerable<UserBadge> Badges { get; set; }
        public IEnumerable<MenuEntryFavorite> Favorites { get; set; }
        public IEnumerable<LastOrder> Last5Orders { get; set; }
        public IEnumerable<UserBalanceAuditItem> Last5BalanceAuditItems { get; set; }
        public IEnumerable<string> Roles { get; set; }

        /// <summary>
        /// A new token for the user in case of user creation in our database.
        /// </summary>
        public string UserToken { get; set; }

        public string ToJson()
        {
            //Return json
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}
