using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lunchorder.Common.Interfaces
{
    public interface IUploadControllerService
    {
        Task<string> UploadImage(Collection<HttpContent> providerContents);
        Task UpdateUserImage(string getUserId, string url);
    }
}