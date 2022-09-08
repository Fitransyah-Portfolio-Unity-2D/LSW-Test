using LSWTest.Shop;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LSWTest.Inventory
{
    public abstract class Item : ScriptableObject, ISerializationCallbackReceiver
    {
        [Tooltip("Auto-generated UUID for saving/loading. Clear this field if you want to generate a new one.")]
        [SerializeField] string itemID = null;
        [Tooltip("Item name to be displayed in UI.")]
        [SerializeField] string displayName = null;
        [Tooltip("Item description to be displayed in UI.")]
        [SerializeField][TextArea] string description = null;
        [Tooltip("The UI icon to represent this item in the inventory.")]
        [SerializeField] Sprite icon = null;
        [Tooltip("The prefab that should be spawned when this item is dropped.")]
        [SerializeField] Pickup pickup = null;
        [Tooltip("If true, multiple items of this type can be stacked in the same inventory slot.")]
        [SerializeField] bool stackable = false;
        [Tooltip("Initial price of this type of item")]
        [SerializeField] float price = 0;
        [Tooltip("Set item category for filter function in shop UI")]
        [SerializeField] ItemCategory category = ItemCategory.None;

        static Dictionary<string, Item> itemLookupCache;

        /// <summary>
        /// Bringing item that has the same ID from resources of Scriptable Object Item
        /// By using its UUID
        /// </summary>
        /// <param name="itemID">
        /// UUID belong to a specific item
        /// </param>
        /// <returns> Item instance that have the same UUID </returns>
        public static Item GetFromID(string itemID)
        {
            if (itemLookupCache == null)
            {
                itemLookupCache = new Dictionary<string, Item>();
                var itemList = Resources.LoadAll<Item>("");
                foreach (var item in itemList)
                {
                    if (itemLookupCache.ContainsKey(item.itemID))
                    {
                        Debug.LogError(string.Format("There is a duplicate itemID from LSWTest.PlayerInventory.Item for objects : {0} and {1}", itemLookupCache[item.itemID], item));
                        continue;
                    }

                    itemLookupCache[item.itemID] = item;
                }
            }

            if (itemID == null || !itemLookupCache.ContainsKey(itemID)) return null;
            return itemLookupCache[itemID];
        }
        /// <summary>
        /// Spawn the pickup prefab into the game world. This method should be called by another script that controlling spawning
        /// Script who called this method should at least present in the world and have Transform component on it
        /// </summary>
        /// <param name="position"> Position is defined by gameobject that handle spawning usually prefab </param>
        /// <returns> Return clone of this Pickup prefab with complete associated Item data data </returns>
        public Pickup SpawnPickup(Vector3 position, int number)
        {
            var pickup = Instantiate(this.pickup);
            var collectibles = GameObject.FindWithTag("Collectibles");
            pickup.transform.position = position;
            pickup.Setup(this, number);
            return pickup;
        }
        public string GetItemID()
        {
            return itemID;
        }
        public string GetDisplayName()
        {
            return displayName;
        }
        public string GetDescription()
        {
            return description;
        }
        public Sprite GetIcon()
        {
            return icon;
        }
        public bool IsStackable()
        {
            return stackable;
        }

        public float GetPrice()
        {
            return price;
        }

        public ItemCategory GetCategory()
        {
            return category;
        }

        #region ISerializationCallbackReceiver
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (String.IsNullOrWhiteSpace(itemID))
            {
                itemID = Guid.NewGuid().ToString();
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            // need by Interface but nothing necessary on this call
        }
        #endregion
    }
}

