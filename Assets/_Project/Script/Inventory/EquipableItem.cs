using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSWTest.Inventory
{
    [CreateAssetMenu(menuName = "LSW Test/PlayerInventory/EquipableItem")]
    public class EquipableItem : Item
    {
        [Tooltip("Where this item allowed to equip")]
        [SerializeField] EquipLocation allowedEquipLocation = EquipLocation.Weapon;

        public EquipLocation GetAllowedEquipLocation()
        {
            return allowedEquipLocation;
        }
    }
}
