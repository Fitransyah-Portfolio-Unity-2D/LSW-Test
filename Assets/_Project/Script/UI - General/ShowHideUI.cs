using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LSWTest.UI
{
    public class ShowHideUI : MonoBehaviour
    {
        [SerializeField] KeyCode toggleInventoryKey = KeyCode.I;
        [SerializeField] GameObject inventoryUIContainer = null;
        [SerializeField] KeyCode toggleShopKey = KeyCode.O;
        [SerializeField] GameObject shopUIContainer = null;

        public delegate void OnShopActivity(GameObject shopUIContainer);
        public OnShopActivity onShopActivity;

        void Start()
        {
            inventoryUIContainer.SetActive(false);
            shopUIContainer.SetActive(false);
            
        }

        void Update()
        {
            if (Input.GetKeyDown(toggleInventoryKey))
            {
                inventoryUIContainer.SetActive(!inventoryUIContainer.activeSelf);
            }

            if (Input.GetKeyDown(toggleShopKey))
            {
                shopUIContainer.SetActive(!shopUIContainer.activeSelf);
                onShopActivity(shopUIContainer);
            }
        }
    }
}


