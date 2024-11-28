using System.Threading.Tasks;

namespace Futura.ApiRequestsBuilder.RequestSenders
{
    public interface IRequestSender
    {
        public Task<T> SendRequest<T>(ApiRequest apiRequest);
    }
}