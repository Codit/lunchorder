using System.Threading.Tasks;

namespace Lunchorder.Common.Interfaces
{
    /// <summary>
    /// Interface for all push token related services
    /// </summary>
    public interface IPushTokenService
    {
        Task SendPushNotification();
    }
}