using UnityEngine;
using GameDevTV.Core.UI.Dragging;

namespace LSWTest.Inventory
{
    public class InventorySlot : MonoBehaviour, IDragContainer<Item>
    {
        [SerializeField]
        InventoryItem itemIcon = null;

        int index;
        Item item;
        PlayerInventory inventory;

        public void Setup(PlayerInventory inventory, int index)
        {
            this.inventory = inventory;
            this.index = index;
            itemIcon.SetItem(inventory.GetItemInSlot(index), inventory.GetNumberInSlot(index));
        }


        #region IDragSource Implementation
        public Item GetItem()
        {
            return inventory.GetItemInSlot(index);
        }
        public int GetNumber()
        {
            return inventory.GetNumberInSlot(index);
        }
        public void RemoveItems(int number)
        {
            inventory.RemoveFromSlot(index, number);
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
            inventory.AddItemToSlot(index, item, number);
        }
        #endregion
    }
}

