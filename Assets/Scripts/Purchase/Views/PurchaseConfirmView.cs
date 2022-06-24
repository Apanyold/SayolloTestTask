using System;
using UnityEngine;
using UnityEngine.UI;

namespace Didenko.Purchase.View
{
    public class PurchaseConfirmView : UIView
    {
        private Action<PaymentInfo> PurchaseConfirmRequested;

        [SerializeField]
        private InputField inputEmai;
        [SerializeField]
        private InputField inputCardNumber;
        [SerializeField]
        private InputField inputExpiraionMonth;
        [SerializeField]
        private InputField inputExpiraionYeal;

        [Space]
        [SerializeField]
        private Button btnConfirm;

        private void Awake()
        {
            btnConfirm.onClick.AddListener(OnConfirmPressed);
        }

        public void Init(Action<PaymentInfo> PurchaseConfirmRequested)
        {
            this.PurchaseConfirmRequested = PurchaseConfirmRequested;
        }

        private void OnConfirmPressed()
        {
            if (!InputValidation())
            {
                Debug.LogError("Input fields invalid");
                return;
            }

            var paymentInfo = new PaymentInfo()
            {
                Email = inputEmai.text,
                CardNumber = inputCardNumber.text,
                ExpirationDate = inputExpiraionMonth.text + "/" + inputExpiraionYeal.text
            };

            PurchaseConfirmRequested?.Invoke(paymentInfo);
        }

        private bool InputValidation()
        {
            bool isEmpty = IsInputEmpty(inputEmai) && IsInputEmpty(inputCardNumber)
                && IsInputEmpty(inputExpiraionMonth) && IsInputEmpty(inputExpiraionYeal);

            return !isEmpty;
        }

        private bool IsInputEmpty(InputField inputField) => string.IsNullOrEmpty(inputField.text);
    }
}