using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSWTest.Inventory
{
    /// <summary>
    /// This script is used to pre-define collectibles that exist in the world
    /// Will be automatically generated Pickup prefab using this script location
    /// As well using the Item type infromation and data
    /// </summary>
    public class PickupSpawner : MonoBehaviour
    {
        [SerializeField] Item item = null;

        private void Awake()
        {
            SpawnPickup();
        }
        void SpawnPickup()
        {
            var spawnedPickup = item.SpawnPickup(transform.position);
            spawnedPickup.transform.SetParent(transform);
        }
    }
}

