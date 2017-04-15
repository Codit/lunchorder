using Lunchorder.Domain.Entities.Authentication;
using Microsoft.AspNet.Identity;


namespace Lunchorder.Api.Infrastructure
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole, string>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store)
            : base(store)
        {
        }
    }
}