using System;

namespace Futura.ApiRequestsBuilder
{
    public struct ApiRequest
    {
        public ApiRequestType Type { get; }
        
        public string BaseUrl { get; }
        
        public string Token { get; }

        public string Endpoint { get; }

        public string JsonBody { get; }

        public Action<string> OnErrorCallback { get; }

        public ApiRequest(ApiRequestType type, string baseUrl, string token, string endpoint, string jsonBody, Action<string> onErrorCallback)
        {
            Type = type;
            BaseUrl = baseUrl;
            Token = token;
            Endpoint = endpoint;
            JsonBody = jsonBody;
            OnErrorCallback = onErrorCallback;
        }
    }
}