using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LSWTest.Inventory
{
    [RequireComponent(typeof(Image))]
    public class InventoryItem : MonoBehaviour
    {
        // CONFIG DATA
        [SerializeField] GameObject textContainer = null;
        [SerializeField] TMP_Text itemNumber = null;

        public void SetItem(Item item)
        {
            SetItem(item, 0);
        }
        /// <summary>
        /// First will check if Item SO reside in slot are null or  not
        /// If null  will disable the Image component, if not null will set up Item SO iconField data and enable the game object.
        /// </summary>
        /// <param name="item">Item data (iconField) to set in the Image component.</param>
        public void SetItem(Item item, int number)
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

            if (itemNumber)
            {
                if (number <= 1)
                {
                    textContainer.SetActive(false);
                }
                else
                {
                    textContainer.SetActive(true);
                    itemNumber.text = number.ToString();
                }
            }
        }
    }
}

