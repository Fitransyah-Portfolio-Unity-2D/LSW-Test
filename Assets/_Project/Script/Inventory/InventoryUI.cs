using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSWTest.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] InventorySlot inventorySlotPrefab;

        PlayerInventory playerInventory;

        private void Awake()
        {
            playerInventory = PlayerInventory.GetPlayerInventory();
            playerInventory.OnInventoryUpdate += Redraw;
        }
        private void Start()
        {
            Redraw();
        }

        void Redraw()
        {
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for(int i = 0; i < playerInventory.GetSize(); i++)
            {
                var itemUI = Instantiate(inventorySlotPrefab, transform);
                itemUI.Setup(playerInventory, i);
            }
        }
    }
}

