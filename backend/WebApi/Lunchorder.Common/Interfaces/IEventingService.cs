namespace Lunchorder.Common.Interfaces
{
    public interface IEventingService
    {
        void SendMessage(Domain.Entities.Eventing.Message message);
    }
}
