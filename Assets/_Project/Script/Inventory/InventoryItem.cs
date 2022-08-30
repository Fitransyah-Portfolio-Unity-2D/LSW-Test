using UnityEngine;
using UnityEngine.UI;

namespace LSWTest.Inventory
{
    [RequireComponent(typeof(Image))]
    public class InventoryItem : MonoBehaviour
    {
        /// <summary>
        /// First will check if Item SO reside in slot are null or  not
        /// If null  will disable the Image component, if not null will set up Item SO icon data and enable the game object.
        /// </summary>
        /// <param name="item">Item data (icon) to set in the Image component.</param>
        public void SetItem(Item item)
        {
            var iconImage = GetComponent<Image>();
            if (item == null)
            {
                iconImage.enabled = false;
            }
            else
            {
                iconImage.enabled = true;
                iconImage.sprite = item.GetIcon();
            }
        }
    }
}

