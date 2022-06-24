using UnityEngine;

namespace Didenko.Purchase.View.Data
{
    public class ShopItemViewData
    {
        public string itemId;
        public Sprite itemImage;
        public string title;
        public double price;
        public string currencySign;

        public ShopItemViewData(ShopItem data)
        {
            this.itemId = data.item_id;
            this.title = data.title;
            this.price = data.price;
            this.currencySign = data.currency_sign;
        }
    }
}
