using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSWTest.Inventory
{
    public class ItemDropper : MonoBehaviour
    {
        
        List<Pickup> droppedItems = new List<Pickup>();

        /// <summary>
        /// Use this in Inventory Drop Target to drop item one by one from Inventory UI
        /// </summary>
        /// <param name="item"></param>
        public void DropItem(Item item)
        {
            SpawnPickup(item, GetDropLocation(),1);
        }
        /// <summary>
        /// Use this in Inventory Drop Target to drop stackable items at once from Inventory UI
        /// </summary>
        /// <param name="item"></param>
        /// <param name="number"></param>
        public void DropItem(Item item, int number)
        {
            SpawnPickup(item, GetDropLocation(), number);
        }
        protected virtual Vector3 GetDropLocation()
        {
            var properSpawnLocation = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
            return properSpawnLocation;
        }
        public void SpawnPickup(Item item, Vector3 spawnLocation, int number)
        {
            var pickup = item.SpawnPickup(spawnLocation, number);
            var collectibles = GameObject.FindWithTag("Collectibles");
            pickup.transform.SetParent(collectibles.transform);
            droppedItems.Add(pickup);
        }       
    }
}

