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

        void Start()
        {
            inventoryUIContainer.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(toggleInventoryKey))
            {
                inventoryUIContainer.SetActive(!inventoryUIContainer.activeSelf);
            }
        }
    }
}


