using UnityEngine;
using System;
using RSG;
using Didenko.Purchase.View.Data;

namespace Didenko.Purchase.View
{
    public class ShopView : UIView
    {
        public Func<Sprite> ImageRequested;

        public ShopItemView shopItemPrefab;

        private Func<string, IPromise<Sprite>> getImageDelegate;

        private Action<string> itemBuyPressed;

        [SerializeField]
        private Transform itemsHolder;

        public void Init(Func<string, IPromise<Sprite>> getImageDelegate,
            Action<string> onItemBuyPressed)
        {
            this.getImageDelegate = getImageDelegate;
            this.itemBuyPressed = onItemBuyPressed;
        }

        public void AddItem(ShopItem shopItem)
        {
            var shopItemView = Instantiate(shopItemPrefab, itemsHolder);

            if (!string.IsNullOrEmpty(shopItem.item_image))
                getImageDelegate?.Invoke(shopItem.item_image)
                    .Then(sprite => shopItemView.SetImage(sprite))
                    .Catch(e => shopItemView.gameObject.SetActive(false));

            var viewData = new ShopItemViewData(shopItem);
            shopItemView.Init(viewData, itemBuyPressed);
        }
    }
}
