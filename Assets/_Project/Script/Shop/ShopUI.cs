using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

namespace LSWTest.Shop
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] TMP_Text shopName;
        [SerializeField] Transform listRoot;
        [SerializeField] RowUI rowPrefab;
        
        Shopper shopper = null;
        Shop currentShop = null;
        private void Start()
        {
            shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();

            if (shopper == null) return;

            shopper.activeShopChange += ShopChanged;

            ShopChanged();
        }

        public void ShopClosed()
        {
            shopper.RemoveActiveShop();
        }

        void ShopChanged()
        {
            if (currentShop != null)
            {
                currentShop.onChange -= RefreshUI;
            }
            
            currentShop = shopper.GetActiveShop();
            gameObject.SetActive(currentShop != null);

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
        }

    }
}
