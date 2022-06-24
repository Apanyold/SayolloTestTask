using UnityEngine;
using System;
using RSG;
using UnityEngine.UI;
using Didenko.Purchase;

namespace Didenko.Purchase.View
{
    public class PurchaseUIManager : MonoBehaviour
    {
        [SerializeField]
        public Button btnStartPurchase;

        [SerializeField]
        public ShopView shopView;

        [SerializeField]
        public PurchaseConfirmView confirmView;

        private Action<string> ItemBuyPressed;
        private Action<PaymentInfo> PurchaseConfirmRequested;
        private Func<IPromise<ShopItem>> PurchaseStart;

        private void Start()
        {
            confirmView.Hide();
            shopView.Hide();

            btnStartPurchase.onClick.AddListener(OnStartButtonClick);
        }

        public void Init(Func<string, IPromise<Sprite>> getImageDelegate,
            Action<PaymentInfo> PurchaseConfirmRequested,
            Func<IPromise<ShopItem>> PurchaseStart)
        {
            this.PurchaseConfirmRequested = PurchaseConfirmRequested;
            this.PurchaseStart = PurchaseStart;

            shopView.Init(getImageDelegate, OnItemBuyPressed);
            confirmView.Init(OnPurchaseConfirmRequested);
        }

        public void OnItemBuyPressed(string itemId)
        {
            shopView.Hide();
            confirmView.Show();
        }

        public void OnPurchaseConfirmRequested(PaymentInfo paymentInfo)
        {
            PurchaseConfirmRequested.Invoke(paymentInfo);

            shopView.Show();
            confirmView.Hide();
        }

        private void OnStartButtonClick()
        {
            PurchaseStart.Invoke().Then(item =>
            {
                shopView.Show();
                shopView.AddItem(item);

                btnStartPurchase.gameObject.SetActive(false);
            });
        }
    }
}
