using System.Threading.Tasks;

namespace Lunchorder.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendHtmlEmail(string subject, string toEmail, string content);
    }
}