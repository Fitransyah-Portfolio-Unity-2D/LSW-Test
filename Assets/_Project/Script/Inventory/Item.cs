using System;
using System.Collections.Generic;
using UnityEngine;

namespace LSWTest.Inventory
{
    [CreateAssetMenu(menuName = "LSW Test/Inventory/Item")]
    public class Item : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] string itemID = null;
        [SerializeField] string displayName = null;
        [SerializeField] string description = null;
        [SerializeField] Sprite icon = null;
        [SerializeField] bool stackable = false;

        static Dictionary<string, Item> itemLookupCache;

        /// <summary>
        /// Bringing item that has the same ID from resources of Scriptable Object Item
        /// By using its UUID
        /// </summary>
        /// <param name="itemID">
        /// UUID belong to a specific item
        /// </param>
        /// <returns>
        /// Item instance that have the same UUID
        /// </returns>
        public static Item GetFromID(string itemID)
        {
            if (itemLookupCache == null)
            {
                itemLookupCache = new Dictionary<string, Item>();
                var itemList = Resources.LoadAll<Item>("_Project");
                foreach (var item in itemList)
                {
                    if (itemLookupCache.ContainsKey(item.itemID))
                    {
                        Debug.LogError(string.Format("There is a duplicate itemID from LSWTest.Inventory.Item for objects : {0} and {1}", itemLookupCache[item.itemID], item));
                        continue;
                    }

                    itemLookupCache[item.itemID] = item;
                }
            }

            if (itemID == null || !itemLookupCache.ContainsKey(itemID)) return null;
            return itemLookupCache[itemID];
        }



        #region ISerializationCallbackReceiver
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (String.IsNullOrWhiteSpace(itemID) || String.IsNullOrEmpty(itemID))
            {
                itemID = System.Guid.NewGuid().ToString();
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            // need by Interface but nothing necessary on this call
        }
        #endregion
    }
}

