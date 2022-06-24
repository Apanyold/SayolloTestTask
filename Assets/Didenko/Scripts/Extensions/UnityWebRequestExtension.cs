using UnityEngine.Networking;
using System.Threading.Tasks;
using System;

namespace Didenko.Extensions
{
    public static class UnityWebRequestExtension
    {
        public static async void SendRequestAsync(this UnityWebRequest @this, Action<UnityWebRequest> callback)
        {
            @this.SendWebRequest();

            while (!@this.isDone)
                await Task.Yield();

            callback.Invoke(@this);
        }
    }
}