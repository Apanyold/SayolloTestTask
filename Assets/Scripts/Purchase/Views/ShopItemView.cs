using UnityEngine;
using UnityEngine.UI;
using System;
using Didenko.Purchase.View.Data;

namespace Didenko.Purchase.View
{
    public class ShopItemView : MonoBehaviour
    {
        public string ItemId => itemId;
        private string itemId;

        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private Text title;

        [SerializeField]
        private Text price;

        [SerializeField]
        private Button buyButton;

        public void Init(ShopItemViewData data, Action<string> BuyPressed)
        {
            itemId = data.itemId;

            if (data.itemImage != null)
                SetImage(data.itemImage);

            title.text = data.title;
            price.text = data.price.ToString() + " " + data.currencySign;

            AddListners(BuyPressed);
        }

        public void SetImage(Sprite sprite)
        {
            itemImage.sprite = sprite;
        }

        private void AddListners(Action<string> BuyPressed)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() => BuyButtonPressed(BuyPressed));
        }

        private void BuyButtonPressed(Action<string> PurchasePressed)
        {
            PurchasePressed.Invoke(ItemId);
        }
    }
}
