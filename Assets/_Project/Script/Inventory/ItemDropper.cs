using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSWTest.Inventory
{
    public class ItemDropper : MonoBehaviour
    {
        // this will be usefull for saving system
        List<Pickup> droppedItems = new List<Pickup>();
        public void DropItem(Item item)
        {
            SpawnPickup(item, GetDropLocation(),1);
        }
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

