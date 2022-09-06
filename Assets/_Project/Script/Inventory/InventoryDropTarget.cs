using GameDevTV.Core.UI.Dragging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace LSWTest.Inventory
{
    /// <summary>
    /// Handles dropping item use previous drag and drop system
    /// Placed in root canvas where UI inventory reside
    /// </summary>
    public class InventoryDropTarget : MonoBehaviour, IDragDestination<Item>
    {
        public void AddItems(Item item, int number)
        {
            var player = GameObject.FindWithTag("Player");
            player.GetComponent<ItemDropper>().DropItem(item, number); 
        }

        public int MaxAcceptable(Item item)
        {
            return int.MaxValue;
        }
    }
}

