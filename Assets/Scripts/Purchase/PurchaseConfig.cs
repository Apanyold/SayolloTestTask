using UnityEngine;

namespace Didenko.Purchase
{
    [CreateAssetMenu(fileName = "PurchaseConfig", menuName = "ScriptableObjects/PurchaseConfig")]
    public class PurchaseConfig : ScriptableObject
    {
        public string apiRoot;
        public string shopItems;
        public string purchaseConfirm;
        public string workSpace;
    }
}

