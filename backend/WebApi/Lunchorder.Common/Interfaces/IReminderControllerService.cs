using System.Security.Claims;
using System.Threading.Tasks;
using Lunchorder.Domain.Dtos;
using Lunchorder.Domain.Dtos.Requests;

namespace Lunchorder.Common.Interfaces
{
    public interface IReminderControllerService
    {
        Task Register(string token, ClaimsIdentity claimsIdentity);
        Task SetReminder(Reminder request, ClaimsIdentity claimsIdentity);
        Task DeleteReminder(int type, ClaimsIdentity claimsIdentity);
    }
}