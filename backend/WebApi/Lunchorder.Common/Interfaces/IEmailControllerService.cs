using System.Threading.Tasks;

namespace Lunchorder.Common.Interfaces
{
    public interface IEmailControllerService
    {
        Task<bool> SendEmail();
    }
}