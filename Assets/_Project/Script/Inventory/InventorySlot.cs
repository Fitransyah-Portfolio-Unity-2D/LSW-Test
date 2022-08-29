using UnityEngine;
using GameDevTV.Core.UI.Dragging;

namespace LSWTest.Inventory
{
    public class InventorySlot : MonoBehaviour, IDragContainer<Sprite>
    {
        [SerializeField]
        InventoryItem itemIcon;
        public void AddItems(Sprite item, int number)
        {
            itemIcon.SetItem(item);
        }

        public Sprite GetItem()
        {
            return itemIcon.GetItem();
        }

        public int GetNumber()
        {
            // temporarily will return 1 untill we are working with
            // stackable items
            return 1;
        }

        public int MaxAcceptable(Sprite item)
        {
            if (GetItem() == null)
            {
                return int.MaxValue;
            }
            return 0;
        }

        public void RemoveItems(int number)
        {
            itemIcon.SetItem(null);
        }
    }
}

