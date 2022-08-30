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
        /// <summary>
        /// Easy way to get the Player Inventory script
        /// </summary>
        /// <returns></returns>
        public static Inventory GetPlayerInventory()
        {
            // should player Inventory.cs
            var player = GameObject.FindWithTag("Player");
            return player.GetComponent<Inventory>();
        }
        /// <summary>
        /// To know how many player inventory size/limit
        /// </summary>
        /// <returns></returns>
        public int GetSize()
        {
            return slots.Length;
        }
        public Item GetItemInSlot(int slot)
        {
            return slots[slot];
        }
        public void RemoveFromSlot(int slot)
        {
            slots[slot] = null;
            if (OnInventoryUpdate != null)
            {
                OnInventoryUpdate();
            }
        }
        public bool AddItemToSlot(int slot, Item item)
        {
            if (slots[slot] != null)
            {
                return AddToFirstEmptySlot(item);
            }

            slots[slot] = item;

            if(OnInventoryUpdate != null)
            {
                OnInventoryUpdate();
            }
            return true;
        }
        public bool AddToFirstEmptySlot(Item item)
        {
            int i = FindSlot(item);

            if (i < 0)
            {
                return false;
            }

            slots[i] = item;
            if (OnInventoryUpdate != null)
            {
                OnInventoryUpdate();
            }
            return true;
        }
        public bool HasSpaceFor(Item item)
        {
            return FindSlot(item) >= 0;
        }


        #region Private Methods
        private void Awake()
        {
            slots = new Item[inventorySize];
        }
        int FindSlot(Item item)
        {
            return FindEmptySlot();
        }
        int FindEmptySlot()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

    }
}

