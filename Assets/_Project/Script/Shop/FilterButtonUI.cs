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
        void Awake()
        {
            button = GetComponent<Button>();

            button.onClick.AddListener(SelectFilter);
        }

        void Start()
        {
            
        }

        public void SetShop(Shop currentShop)
        {
            this.currentShop = currentShop;
        }

        public void RefreshUI()
        {
            button.interactable = currentShop.GetFilter() != category;
        }

        private void SelectFilter()
        {
            currentShop.SelectFilter(category);
        }
    }

}