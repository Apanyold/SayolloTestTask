using UnityEngine;

namespace Didenko.Purchase.View
{
    public abstract class UIView : MonoBehaviour, IUIView
    {
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}