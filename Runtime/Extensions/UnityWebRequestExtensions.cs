using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Futura.ApiRequestsBuilder.Extensions
{
    internal static class UnityWebRequestExtensions
    {
        public static Task<UnityWebRequestAsyncOperation> SendWebRequestAsync(this UnityWebRequest request)
        {
            TaskCompletionSource<UnityWebRequestAsyncOperation> tcs = new();
            UnityWebRequestAsyncOperation operation = request.SendWebRequest();
            Action<AsyncOperation> callback = null;

            callback = _ =>
            {
                operation.completed -= callback;
                tcs.TrySetResult(operation);
            };

            operation.completed += callback;

            return tcs.Task;
        }
    }
}