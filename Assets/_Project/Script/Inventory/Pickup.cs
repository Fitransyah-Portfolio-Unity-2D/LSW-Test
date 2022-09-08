    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSWTest.Inventory
{
    /// <summary>
    /// This script located at every PickupPrefab. Will find and store data on Awake about
    /// the pickup item itself
    /// </summary>
    public class Pickup : MonoBehaviour
    {
        [SerializeField] Item item;
        [SerializeField] PlayerInventory inventory;
        [SerializeField] int number = 1;

        private void Awake()
        {
            var player = GameObject.FindWithTag("Player");
            inventory = player.GetComponent<PlayerInventory>();
        }
        /// <summary>
        /// This method have to be called after Prefab contains this script
        /// Instantiated in the world
        /// </summary>
        /// <param name="item"> Item type data need to be stored in this instance </param>
        public void Setup(Item item, int number)
        {
            this.item = item;  
            
            if (!item.IsStackable())
            {
                number = 1;
            }
            this.number = number;
        }
        /// <summary>
        /// Way to get Item type imformation about this pickup
        /// </summary>
        /// <returns> Item class scriptable object include pre defined data on it </returns>
        public Item GetItem()
        {
            return item;
        }
        public int GetNumber()
        {
            return number;
        }
        /// <summary>
        /// Method to execute pick up mechanic that happening in the world
        /// First will check player PlayerInventory for slot, if there is slot than destroy its physical representation in the world
        /// and store the Item data to player PlayerInventory
        /// </summary>
        public void PickupItem()
        {
            bool foundSlot = inventory.AddToFirstEmptySlot(item, number);
            if (foundSlot)
            {
                Destroy(gameObject);
            }
        }
        /// <summary>
        /// Checking player inventory for this item type exist or not
        /// and if its exist are that type of Item is stackable or not?
        /// </summary>
        /// <returns> Return True based result on PlayerInventory.HasSpaceFor method </returns>
        public bool CanBePickedUp()
        {
            return inventory.HasSpaceFor(item);
        }
    }
}

