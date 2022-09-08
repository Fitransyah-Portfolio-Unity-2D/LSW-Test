using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LSWTest.Shop
{
    public class FilterButtonUI : MonoBehaviour
    {
        [SerializeField] ItemCategory category = ItemCategory.None; 

        Button button;
        Shop currentShop;
        void Start()
        {
            button = GetComponent<Button>();

            button.onClick.AddListener(SelectFilter);
        }

        public void SetShop(Shop currentShop)
        {
            this.currentShop = currentShop;
        }

        private void SelectFilter()
        {
            currentShop.SelectFilter(category);
        }
    }

}