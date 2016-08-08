using System.Threading.Tasks;

namespace Lunchorder.Common.Interfaces
{
    public interface IMessagingService
    {
        Task SendMessageAsync(Domain.Entities.Eventing.Message message);
    }
}