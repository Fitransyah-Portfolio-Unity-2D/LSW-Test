using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSWTest.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] InventorySlot inventorySlotPrefab;

        Inventory playerInventory;

        private void Awake()
        {
            playerInventory = GetComponent<Inventory>();
        }
    }
}

