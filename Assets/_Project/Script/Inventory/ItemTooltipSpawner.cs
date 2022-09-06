using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Core.UI.Tooltips;

namespace LSWTest.Inventory
{
    public class ItemTooltipSpawner : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            // Best practice to use interface that present in all class that need tooltip
            // instead directly call the class name like this
            var item = GetComponent<InventorySlot>().GetItem();
            if (!item) return  false;

            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            var itemTooltip = tooltip.GetComponent<ItemTooltip>();
            if (itemTooltip != null) ;

            var item = GetComponent<InventorySlot>().GetItem();

            itemTooltip.Setup(item);
        }
    }
}
