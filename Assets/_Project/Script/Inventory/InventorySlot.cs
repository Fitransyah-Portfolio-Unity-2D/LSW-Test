using UnityEngine;
using GameDevTV.Core.UI.Dragging;

namespace LSWTest.Inventory
{
    public class InventorySlot : MonoBehaviour, IDragContainer<Item>
    {
        [SerializeField]
        InventoryItem itemIcon = null;

        int index;
        Inventory inventory;

        public void Setup(Inventory inventory, int index)
        {
            this.inventory = inventory;
            this.index = index;
            itemIcon.SetItem(inventory.GetItemInSlot(index));
        }


        #region IDragSource Implementation
        public Item GetItem()
        {
            return inventory.GetItemInSlot(index);
        }
        public int GetNumber()
        {
            // temporarily will return 1 untill we are working with
            // stackable items
            return 1;
        }
        public void RemoveItems(int number)
        {
            inventory.RemoveFromSlot(index);
        }
        #endregion



        #region IDragDestination Implementation
        public int MaxAcceptable(Item item)
        {
            if (inventory.HasSpaceFor(item))
            {
                return int.MaxValue;
            }
            return 0;
        }
        public void AddItems(Item item, int number)
        {
            inventory.AddItemToSlot(index, item);
        }
        #endregion
    }
}

