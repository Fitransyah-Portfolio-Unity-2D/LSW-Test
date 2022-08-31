using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSWTest.Inventory
{
    public class ItemDropper : MonoBehaviour
    {
        List<Pickup> droppedItems = new List<Pickup>();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DropItem(Item.GetFromID("8649d11f-e595-4eb4-8d8c-03a8eb00cb52"));
            }
        }
        public void DropItem(Item item)
        {
            SpawnPickup(item, GetDropLocation());
        }
        protected virtual Vector3 GetDropLocation()
        {
            var properSpawnLocation = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
            return properSpawnLocation;
        }
        public void SpawnPickup(Item item, Vector3 spawnLocation)
        {
            var pickup = item.SpawnPickup(spawnLocation);
            droppedItems.Add(pickup);
        }       
    }
}

