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
        Item item;
        Inventory inventory;

        private void Awake()
        {
            var player = GameObject.FindWithTag("Player");
            inventory = player.GetComponent<Inventory>();
        }
        /// <summary>
        /// This method have to be called after Prefab contains this script
        /// Instantiated in the world
        /// </summary>
        /// <param name="item"> Item type data need to be stored in this instance </param>
        public void Setup(Item item)
        {
            this.item = item;   
        }
        /// <summary>
        /// Way to get Item type imformation about this pickup
        /// </summary>
        /// <returns> Item class scriptable object include pre defined data on it </returns>
        public Item GetItem()
        {
            return item;
        }
        /// <summary>
        /// Method to execute pick up mechanic that happening in the world
        /// First will check player Inventory for slot, if there is slot than destroy its physical representation in the world
        /// and store the Item data to player Inventory
        /// </summary>
        public void PickupItem()
        {
            bool foundSlot = inventory.AddToFirstEmptySlot(item);
            if (foundSlot)
            {
                Destroy(gameObject);
            }
        }
        /// <summary>
        /// Checking player inventory for this item type exist or not
        /// and if its exist are that type of Item is stackable or not?
        /// </summary>
        /// <returns> Return True based result on Inventory.HasSpaceFor method </returns>
        public bool CanBePickedUp()
        {
            return inventory.HasSpaceFor(item);
        }
    }
}

