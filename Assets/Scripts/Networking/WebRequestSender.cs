using UnityEngine.Networking;
using RSG;
using System;
using Didenko.Extensions;
using UnityEngine;
using System.Text;
using Newtonsoft.Json;

namespace Didenko.Networking
{
    public class WebRequestSender
    {
        public IPromise<DownloadHandler> GetRequest(string url)
        {
            var promise = new Promise<DownloadHandler>();
            var request = UnityWebRequest.Get(url);
            //{
                request.SendRequestAsync(response =>
                {
                    if (response.isNetworkError || response.isHttpError)
                    {
                        promise.LogReject(new Exception(response.error));
                        return;
                    }

                    Debug.Log("Get request recived with url:" + url);
                    promise.Resolve(response.downloadHandler);
                });
            //};

            return promise;
        }

        public IPromise<T> GetXmlRequest<T>(string url) where T : class
        {
            var promise = new Promise<T>();
            GetRequest(url)
                .Then(data =>
                {
                    try
                    {
                        var result = data.text.DeserializeXml<T>();
                        promise.Resolve(result);
                    }
                    catch (Exception e)
                    {
                        promise.LogReject(e);
                    }
                })
                .Catch(promise.Reject);

            return promise;
        }

        public IPromise<T> PostRequest<T>(string url, string json)
        {
            var promise = new Promise<T>();

            PostRequestHandler(url, json)
                .Then(hanlder => 
                {
                    try
                    {
                        var data = JsonConvert.DeserializeObject<T>(hanlder.text);
                        promise.Resolve(data);

                    }
                    catch (Exception e)
                    {
                        promise.LogReject(e);
                        return;
                    }
                })
                .Catch(promise.Reject);

            return promise;
        }

        public IPromise<string> PostRequest(string url, string json)
        {
            var promise = new Promise<string>();

            PostRequestHandler(url, json)
                .Then(handler =>
                {
                    promise.Resolve(handler.text);
                })
                .Catch(promise.Reject);

            return promise;
        }

        public IPromise<DownloadHandler> PostRequestHandler(string url, string json)
        {
            var promise = new Promise<DownloadHandler>();

            var request = new UnityWebRequest(url, "POST");
            //{
                var jsonToSend = new UTF8Encoding().GetBytes(json);
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                request.SendRequestAsync(response =>
                {
                    if (response.isNetworkError || response.isHttpError)
                    {
                        promise.LogReject(new Exception(response.error));
                        return;
                    }
                    promise.Resolve(response.downloadHandler);
                });
            //}

            return promise;
        }
    }
}