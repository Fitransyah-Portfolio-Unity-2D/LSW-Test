using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LSWTest.Shop
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] TMP_Text shopName;
        
        Shopper shopper = null;
        Shop currentShop = null;
        private void Start()
        {
            shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();

            if (shopper == null) return;

            shopper.activeShopChange += ShopChanged;

            ShopChanged();
        }

        void ShopChanged()
        {
            currentShop = shopper.GetActiveShop();
            gameObject.SetActive(currentShop != null);

            if (currentShop == null) return ;

            shopName.text = currentShop.GetShopName();
        }
        public void ShopClosed()
        {
            shopper.RemoveActiveShop();
        }
    }
}
