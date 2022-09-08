using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LSWTest.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] int inventorySize = 16;
        [SerializeField]
        InventorySlot[] slots;
        [Serializable]
        struct InventorySlot
        {
            public Item item;
            public int number;
        }

        public event Action OnInventoryUpdate;
        /// <summary>
        /// Easy way to get the Player PlayerInventory script
        /// </summary>
        /// <returns></returns>
        public static PlayerInventory GetPlayerInventory()
        {
            // should player PlayerInventory.cs
            var player = GameObject.FindWithTag("Player");
            return player.GetComponent<PlayerInventory>();
        }
        /// <summary>
        /// To know how many player inventory size/limit
        /// </summary>
        /// <returns></returns>
        public int GetSize()
        {
            return slots.Length;
        }
        /// <summary>
        /// To get specific Item with slot reference
        /// </summary>
        /// <param name="slot"></param>
        /// <returns> Return the Item correspondence to the mention slot </returns>
        public Item GetItemInSlot(int slot)
        {
            return slots[slot].item;
        }
        public int GetNumberInSlot(int slot)
        {
            return slots[slot].number;
        }
        /// <summary>
        /// Remove item with slot reference
        /// </summary>
        /// <param name="slot"></param>
        public void RemoveFromSlot(int slot, int number)
        {
            slots[slot].number -= number;
            if (slots[slot].number <= 0)
            {
                slots[slot].number = 0;
                slots[slot].item = null;
            }
            
            if (OnInventoryUpdate != null)
            {
                OnInventoryUpdate();
            }
        }
        /// <summary>
        /// To add Item into reference slot, if slot is empty
        /// If stack is not empty and this Item tipe exist will add to following slot
        /// If stack is not empty and this Item tipe not exist will try to find first empty slot from index 0
        /// Eventhough return type is boolean but action still executed within this methods
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="item"></param>
        /// <returns> True value when success putting Item anywhere in the slot </returns>
        public bool AddItemToSlot(int slot, Item item, int number)
        {
            if (slots[slot].item != null)
            {
                return AddToFirstEmptySlot(item, number);
            }

            var i = FindStack(item);
            if (i >= 0)
            {
                slot = i;
            }

            slots[slot].item = item;
            slots[slot].number += number;

            if(OnInventoryUpdate != null)
            {
                OnInventoryUpdate();
            }
            return true;
        }
        /// <summary>
        /// Search PlayerInventory if any available space for this Item
        /// </summary>
        /// <param name="item"></param>
        /// <returns> Return True if space is available </returns>
        public bool HasSpaceFor(Item item)
        {
            return FindSlot(item) >= 0;
        }
        /// <summary>
        /// First checking inventory if any slot available, if not the will return thsi function as False
        /// If slot available will put the Item reference into slot that available
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <param name="number">Number of item to add</param>
        /// <returns> Return True when succes putting the item and False when the attempt is failed </returns>
        public bool AddToFirstEmptySlot(Item item, int number)
        {
            int i = FindSlot(item);

            if (i < 0)
            {
                return false;
            }

            slots[i].item = item;
            slots[i].number += number;

            if (OnInventoryUpdate != null)
            {
                OnInventoryUpdate();
            }
            return true;
        }
        /// <summary>
        /// Search PlayerInventory if it has this Item type
        /// </summary>
        /// <param name="item"></param>
        /// <returns> Return True when Item is exist in PlayerInventory </returns>
        public bool HasItem(Item item)
        {
            for(int i = 0; i < slots.Length; i++)
            {
                if (object.ReferenceEquals(slots[i].item, item))
                {
                    return true;
                }
            }
            return false;
        }


        #region Private Methods
        private void Awake()
        {
            slots = new InventorySlot[inventorySize];

            // This is where to setup Player Item before game start 
            // slots size/index is depend on "inventorySize"
            // use itemID that generated in every Item Scriptable Object
            slots[0].item = Item.GetFromID("0868566c-009f-487f-896c-aa50b8691d43");
            slots[0].number = 1;
            slots[1].item = Item.GetFromID("ceb7ef13-d4c4-4af0-8c8a-30fc8b65d6e1");
            slots[1].number = 1;

        }
        public int FindSlot(Item item)
        {
            int i = FindStack(item);
            if (i < 0)
            {
                i = FindEmptySlot();
            }
            return i;
        }
        int FindEmptySlot()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)
                {
                    return i;
                }
            }
            return -1;
        }
        private int FindStack(Item item)
        {
            if (!item.IsStackable())
            {
                return -1;
            }

            for (int i = 0; i < slots.Length; i++)
            {
               if (Object.ReferenceEquals(slots[i].item, item))
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

    }
}

