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

        public void Setup(ShopItem item)
        {
            nameField.text = item.GetName();
            iconField.sprite = item.GetIcon();
            availabilityField.text = $"{item.GetAvailablity()}";
            priceField.text = $"${item.GetPrice():N2}";
        }
    }
}
