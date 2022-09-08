using LSWTest.Inventory;
using System;
using System.Collections;
using UnityEngine;

namespace LSWTest.Shop
{
    [Serializable]
    public class ShopItem
    {
        [SerializeField] Item item;
        [SerializeField] int availability;
        [SerializeField] float price;
        [SerializeField] int quantitiyInTransaction;

        public ShopItem(Item item, int availability, float price, int quantityIntransaction)
        {
            this.item = item;
            this.availability = availability;
            this.price = price;
            this.quantitiyInTransaction = quantityIntransaction;
        }

        public string GetName()
        {
            return item.GetDisplayName();
        }

        public int GetAvailablity()
        {
            return availability;
        }

        public Sprite GetIcon()
        {
            return item.GetIcon();
        }

        public float GetPrice()
        {
            return price;
        }

        public Item GetInventoryItem()
        {
            return item;
        }

        public int GetQuantity()
        {
            return quantitiyInTransaction;
        }
    }
}