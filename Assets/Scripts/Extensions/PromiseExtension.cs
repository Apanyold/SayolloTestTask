using RSG;
using UnityEngine;
using System;

namespace Didenko.Extensions
{
    public static class PromiseExtension
    {
        public static Promise LogReject(this Promise promise, Exception e)
        {
            Debug.LogError(e.Message);
            promise.Reject(e);
            return promise;
        }

        public static Promise<T> LogReject<T>(this Promise<T> promise, Exception e)
        {
            Debug.LogError(e.Message + "\n" + e?.InnerException);
            promise.Reject(e);
            return promise;
        }
    }
}
