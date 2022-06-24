using UnityEngine;

namespace Didenko.Purchase
{
    [CreateAssetMenu(fileName = "PurchaseConfig", menuName = "ScriptableObjects/PurchaseConfig")]
    public class PurchaseConfig : ScriptableObject
    {
        public string rootUrl;
        public string workSpace;
        public string shopItems;
        public string purchaseConfirm;
    }
}

