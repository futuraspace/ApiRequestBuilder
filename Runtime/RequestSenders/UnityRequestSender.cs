using System;
using System.Text;
using System.Threading.Tasks;
using Futura.ApiRequestsBuilder.Extensions;
using UnityEngine;
using UnityEngine.Networking;
using Logger = Futura.ApiRequestsBuilder.Loggers.Logger;

namespace Futura.ApiRequestsBuilder.RequestSenders
{
    public class UnityRequestSender : IRequestSender
    {
        private const string JSON_CONTENT_TYPE = "application/json";

        private readonly ArgumentException _unsupportedRequestException = new ArgumentException("Unsupported request type");

        public async Task<T> SendRequest<T>(ApiRequest apiRequest)
        {
            try
            {
                using UnityWebRequest request = CreateRequest(apiRequest);

                await request.SendWebRequestAsync();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Logger.LogError($"Error: {request.error} / {request.downloadHandler.text}");
                    apiRequest.OnErrorCallback?.Invoke($"Request Callback failed: {request.error}");

                    try
                    {
                        return JsonUtility.FromJson<T>(request.downloadHandler.text);
                    }
                    catch (Exception)
                    {
                        return await Task.FromResult(default(T));
                    }
                }

                if (string.IsNullOrEmpty(request.downloadHandler.text))
                {
                    return await Task.FromResult(default(T));
                }

                Logger.LogInfo($"Api response: {request.downloadHandler.text}");
                return JsonUtility.FromJson<T>(request.downloadHandler.text);
            }
            catch (Exception e)
            {
                apiRequest.OnErrorCallback?.Invoke($"Exception: {e.Message}");
                return await Task.FromResult(default(T));
            }
        }

        private UnityWebRequest CreateRequest(ApiRequest apiRequest)
        {
            UnityWebRequest request = CreateRequestByType(apiRequest);

            if (!string.IsNullOrEmpty(apiRequest.Token))
            {
                request.SetRequestHeader("Authorization", $"Bearer {apiRequest.Token}");
            }

            request.SetRequestHeader("Content-Type", JSON_CONTENT_TYPE);

            return request;
        }

        private UnityWebRequest CreateRequestByType(ApiRequest apiRequest)
        {
            string fullURL = $"{apiRequest.BaseUrl}{apiRequest.Endpoint}";
            Logger.LogInfo("Request URL: " + fullURL);

            switch (apiRequest.Type)
            {
                case ApiRequestType.Get:
                    return UnityWebRequest.Get(fullURL);
                case ApiRequestType.Post:
                    return UnityWebRequest.Post(fullURL, apiRequest.JsonBody, JSON_CONTENT_TYPE);
                case ApiRequestType.Delete:
                    return UnityWebRequest.Delete(fullURL);
                case ApiRequestType.Patch:
                    UnityWebRequest request = new UnityWebRequest(fullURL, "PATCH");
                    request.downloadHandler = new DownloadHandlerBuffer();

                    if (!string.IsNullOrEmpty(apiRequest.JsonBody))
                    {
                        byte[] bodyRaw = Encoding.UTF8.GetBytes(apiRequest.JsonBody);
                        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                    }

                    return request;
                case ApiRequestType.Put:
                    return UnityWebRequest.Put(fullURL, apiRequest.JsonBody);
                default:
                    throw _unsupportedRequestException;
            }
        }
    }
}