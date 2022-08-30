using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSWTest.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] int inventorySize = 16;

        Item[] slots;

        public event Action OnInventoryUpdate;

        public Item GetPlayerInventory()
        {
            return null;
            // should give this inventory list of item
        }
    }
}

