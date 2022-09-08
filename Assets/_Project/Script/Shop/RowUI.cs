using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LSWTest.Shop
{
    public class RowUI : MonoBehaviour
    {
        [SerializeField] Image iconField;
        [SerializeField] TMP_Text nameField;
        [SerializeField] TMP_Text availabilityField;
        [SerializeField] TMP_Text priceField;
        [SerializeField] TMP_Text quantityField;

        [SerializeField] Shop currentShop;
        [SerializeField] ShopItem item;

        public void Setup(Shop currentShop, ShopItem item)
        {
            this.currentShop = currentShop;
            this.item = item;   
            
            nameField.text = item.GetName();
            iconField.sprite = item.GetIcon();
            availabilityField.text = $"{item.GetAvailablity()}";
            priceField.text = $"${item.GetPrice():N2}";
            quantityField.text = $"{item.GetQuantityInTransaction()}";
        }

        public void Add()
        {
            currentShop.AddToTransaction(item.GetInventoryItem(), 1);
        }

        public void Remove()
        {
            currentShop.AddToTransaction(item.GetInventoryItem(), -1);
        }
    }
}
