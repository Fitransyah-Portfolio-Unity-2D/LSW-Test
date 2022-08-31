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

        public Pickup GetPickup()
        {
            return GetComponentInChildren<Pickup>();
        }
        /// <summary>
        /// Information whether this item/pickup need to present in the world or not
        /// </summary>
        /// <returns> Return True once the pickup is collected </returns>
        public bool IsCollected()
        {
            return GetPickup() == null;
        }

        void SpawnPickup()
        {
            var spawnedPickup = item.SpawnPickup(transform.position);
            spawnedPickup.transform.SetParent(transform);
        }
        void DestroyPickup()
        {
            if (GetPickup())
            {
                Destroy(GetPickup().gameObject);
            }
        }
    }
}

