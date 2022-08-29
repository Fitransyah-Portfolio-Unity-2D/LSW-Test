using UnityEngine;
using UnityEngine.UI;

namespace LSWTest.Inventory
{
    [RequireComponent(typeof(Image))]
    public class InventoryItem : MonoBehaviour
    {
        /// <summary>
        /// First will check if parameter is null or not.
        /// If null  will disable the Image component, if not null will set up sprite with parameters and enable the game object.
        /// </summary>
        /// <param name="item">Sprite to set in the Image component.</param>
        public void SetItem(Sprite item)
        {
            var iconImage = GetComponent<Image>();
            if (item == null)
            {
                iconImage.enabled = false;
            }
            else
            {
                iconImage.enabled = true;
                iconImage.sprite = item;
            }
        }
        /// <summary>
        /// If this script is not enable will return null, if enabled will return attached sprite
        /// </summary>        
        /// <returns> <strong>The Sprite currently in Image component</strong> </returns>

        public Sprite GetItem()
        {
            var iconImage = GetComponent<Image>();
            if (!iconImage.enabled)
            {
                return null;
            }
            return iconImage.sprite;
        }
    }
}

