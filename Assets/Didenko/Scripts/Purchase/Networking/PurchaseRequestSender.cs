using Didenko.Extensions;
using Didenko.Networking;
using Didenko.Purchase;
using Newtonsoft.Json;
using RSG;
using UnityEngine;

namespace Didenko.Purchase.Networking
{
    public class PurchaseRequestSender : MonoBehaviour
    {
        [SerializeField]
        private PurchaseConfig purchaseConfig;

        private WebRequestSender requestSender;

        void Start()
        {
            requestSender = new WebRequestSender();
        }

        public IPromise<ShopItem> OnItemRequested(string json)
        {
            var promise = new Promise<ShopItem>();
            var requestUrl = $"{purchaseConfig.rootUrl}/{purchaseConfig.workSpace}/{purchaseConfig.shopItems}";

            requestSender.PostRequest<ShopItem>(requestUrl, json)
                .Then((item) =>
                {
                    promise.Resolve(item);
                })
                .Catch(promise.Reject);

            return promise;
        }

        public IPromise<string> OnPurchaseRequested(string json)
        {
            var promise = new Promise<string>();
            var requestUrl = $"{purchaseConfig.rootUrl}/{purchaseConfig.workSpace}/{purchaseConfig.purchaseConfirm}";

            requestSender.PostRequest(requestUrl, json)
                .Then(response =>
                {
                    promise.Resolve(response);
                })
                .Catch(promise.Reject);

            return promise;
        }

        public IPromise<Sprite> OnImageRequested(string url)
        {
            var promise = new Promise<Sprite>();

            requestSender.GetRequest(url)
                .Then(handler =>
                {
                    var sprite = handler.data.ToSprite();

                    promise.Resolve(sprite);
                })
                .Catch(promise.Reject);

            return promise;
        }
    }
}
