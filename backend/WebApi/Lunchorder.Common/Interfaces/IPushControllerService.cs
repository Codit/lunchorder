using System.Security.Claims;
using System.Threading.Tasks;

namespace Lunchorder.Common.Interfaces
{
    public interface IPushControllerService
    {
        Task Register(string token, ClaimsIdentity claimsIdentity);
    }
}