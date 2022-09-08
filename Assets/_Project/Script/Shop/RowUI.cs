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
        [SerializeField] Image icon;
        [SerializeField] TMP_Text nameField;
        [SerializeField] TMP_Text availability;
        [SerializeField] TMP_Text price;
        [SerializeField] TMP_Text quantityInTransaction;

        public void Setup(ShopItem item)
        {
            nameField.text = item.GetName();
            icon.sprite = item.GetIcon();
            availability.text = item.GetAvailablity();
            price.text = item.GetPrice();
            quantityInTransaction.text = item.GetQuantityInTransaction();
        }

        private string GetQuantityInTransaction()
        {
            throw new NotImplementedException();
        }
    }
}
