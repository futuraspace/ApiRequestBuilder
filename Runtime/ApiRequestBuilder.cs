using System;
using System.Threading.Tasks;
using Futura.ApiRequestsBuilder.RequestSenders;

namespace Futura.ApiRequestsBuilder
{
    public class ApiRequestBuilder
    {
        #region Fields & Properties
        
        private string _token;

        private string _baseUrl;

        private string _endpoint;

        private string _jsonBody;

        private Action<string> _onErrorCallback;

        private readonly IRequestSender _requestSender;

        #endregion
        
        public ApiRequestBuilder(IRequestSender requestSender)
        {
            _requestSender = requestSender;
        }
        
        public ApiRequestBuilder WithToken(string token)
        {
            _token = token;
            return this;
        }

        public ApiRequestBuilder WithJsonBody(string jsonBody)
        {
            _jsonBody = jsonBody;
            return this;
        }

        public ApiRequestBuilder WithErrorCallback(Action<string> onErrorCallback)
        {
            _onErrorCallback = onErrorCallback;
            return this;
        }

        public Task<string> Get(string baseUrl, string endpoint)
        {
            return Send<string>(ApiRequestType.Get, baseUrl, endpoint);
        }
        
        public Task<T> Get<T>(string baseUrl, string endpoint)
        {
            return Send<T>(ApiRequestType.Get, baseUrl, endpoint);
        }

        public Task<string> Post(string baseUrl, string endpoint)
        {
            return Send<string>(ApiRequestType.Post, baseUrl, endpoint);
        }
        
        public Task<T> Post<T>(string baseUrl, string endpoint)
        {
            return Send<T>(ApiRequestType.Post, baseUrl, endpoint);
        }

        public Task<string> Delete(string baseUrl, string endpoint)
        {
            return Send<string>(ApiRequestType.Delete, baseUrl, endpoint);
        }
        
        public Task<T> Delete<T>(string baseUrl, string endpoint)
        {
            return Send<T>(ApiRequestType.Delete, baseUrl, endpoint);
        }

        public Task<string> Patch(string baseUrl, string endpoint)
        {
            return Send<string>(ApiRequestType.Patch, baseUrl, endpoint);
        }
        
        public Task<T> Patch<T>(string baseUrl, string endpoint)
        {
            return Send<T>(ApiRequestType.Patch, baseUrl, endpoint);
        }

        private Task<T> Send<T>(ApiRequestType requestType, string baseUrl, string endpoint)
        {
            _baseUrl = baseUrl;
            _endpoint = endpoint;
            Task<T> sendRequest = _requestSender.SendRequest<T>(BuildRequest(requestType));
            Clear();
            
            return sendRequest;
        }
        
        private ApiRequest BuildRequest(ApiRequestType requestType)
        {
            return new ApiRequest(requestType, _baseUrl, _token, _endpoint, _jsonBody, _onErrorCallback);
        }

        private void Clear()
        {
            _token = string.Empty;
            _baseUrl = string.Empty;
            _endpoint = string.Empty;
            _jsonBody = string.Empty;
            _onErrorCallback = null;
        }
    }
}