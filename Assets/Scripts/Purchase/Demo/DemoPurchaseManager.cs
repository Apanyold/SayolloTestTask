using Didenko.Purchase;
using Didenko.Purchase.Networking;
using Didenko.Purchase.View;
using Newtonsoft.Json;
using RSG;
using UnityEngine;


namespace Didenko.Purchase.Demo
{
    public class DemoPurchaseManager : MonoBehaviour
    {
        [SerializeField]
        private PurchaseUIManager purchaseViewManager;
        [SerializeField]
        private PurchaseRequestSender purchaseRequestSender;

        void Start()
        {
            purchaseViewManager.Init(ImageRequested, ConfirmRequested, OnItemsRequested);
        }

        private void OnItemsRequested()
        {
            Debug.Log("Item request sended");
            var promise = purchaseRequestSender.OnItemRequested("{}");

            promise
                .Then(item => 
                { 
                    Debug.Log("Item recived with id: " + item.item_id);
                    purchaseViewManager.PurchaseStart(item);
                });
        }

        private IPromise<Sprite> ImageRequested(string url)
        {
            Debug.Log("Image request sended");
            var promise = purchaseRequestSender.OnImageRequested(url);

            promise.Then(sprite =>
            {
                if (sprite != null)
                    Debug.Log("Sprite recived");
            });

            return promise;
        }

        private void ConfirmRequested(PaymentInfo obj)
        {
            Debug.Log("Purchase confirm sended");
            var json = JsonConvert.SerializeObject(obj);
            var promise = purchaseRequestSender.OnPurchaseRequested(json);

            promise.Then(responce => Debug.Log("Purchase responce: " + responce));
        }
    }
}


