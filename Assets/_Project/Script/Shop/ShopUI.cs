using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LSWTest.Shop
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] TMP_Text shopName;
        [SerializeField] Transform listRoot;
        [SerializeField] RowUI rowPrefab;
        [SerializeField] TMP_Text totalField;
        [SerializeField] Button confirmButton;
        [SerializeField] Button switchButton;
        
        Shopper shopper = null;
        Shop currentShop = null;
        private void Start()
        {
            shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();

            if (shopper == null) return;

            shopper.activeShopChange += ShopChanged;
            switchButton.onClick.AddListener(SwitchMode);

            ShopChanged();
        }

        public void ShopClosed()
        {
            shopper.RemoveActiveShop();
        }

        public void ConfirmTransaction()
        {
            currentShop.ConfirmTransaction();
        }

        void ShopChanged()
        {
            if (currentShop != null)
            {
                currentShop.onChange -= RefreshUI;
            }
            
            currentShop = shopper.GetActiveShop();
            gameObject.SetActive(currentShop != null);

            foreach(FilterButtonUI button in GetComponentsInChildren<FilterButtonUI>())
            {
                button.SetShop(currentShop);
            }

            if (currentShop == null) return;

            shopName.text = currentShop.GetShopName();
            
            currentShop.onChange += RefreshUI; 

            RefreshUI();
        }

        void RefreshUI()
        {
            foreach (Transform child in listRoot)
            {
                Destroy(child.gameObject);
            }

            foreach (ShopItem item in currentShop.GetFilteredItems())
            {
                RowUI row = Instantiate<RowUI>(rowPrefab, listRoot);
                row.Setup(currentShop, item);

            }

            totalField.text = $"Total: ${currentShop.TransactionTotal():N2}";

            TMP_Text switchText = switchButton.GetComponentInChildren<TMP_Text>();
            TMP_Text confirmText = confirmButton.GetComponentInChildren<TMP_Text>();
            if (currentShop.IsBuyingMode())
            {
                switchText.text = "Switch to Selling";
                confirmText.text = "Buy";
            }
            else
            {
                switchText.text = "Switch to Buying";
                confirmText.text = "Sell";
            }

            foreach (FilterButtonUI button in GetComponentsInChildren<FilterButtonUI>())
            {
                button.RefreshUI();
            }

        }

        public void SwitchMode()
        {
            currentShop.SelectMode(!currentShop.IsBuyingMode());
        }
    }
}
