using GameDevTV.Core.UI.Dragging;
using LSWTest.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour , IDragContainer<Item>
{
    [SerializeField] InventoryItem inventoryItemIcon;
    [SerializeField] EquipLocation equipLocation = EquipLocation.Weapon;

    EquipableItem item = null;

    public void AddItems(Item item, int number)
    {
        this.item = item as EquipableItem;
        inventoryItemIcon.SetItem(item);
    }

    public Item GetItem()
    {
        return item;
    }

    public int GetNumber()
    {
        if (item == null)
        {
            return 0;
        }
        return 1;
    }

    public int MaxAcceptable(Item item)
    {
        if (!(item is EquipableItem))
        {
            return 0;
        }

        var equiableItem = item as EquipableItem;
        if (equiableItem.GetAllowedEquipLocation() != equipLocation)
        {
            return 0;
        }

        if (this.item != null)
        {
            return 0;
        }
        return 1;
    }

    public void RemoveItems(int number)
    {
        item = null;
        inventoryItemIcon.SetItem(null);
    }
}
