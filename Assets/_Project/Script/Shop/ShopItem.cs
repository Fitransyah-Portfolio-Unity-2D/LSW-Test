using LSWTest.Inventory;
using System;
using System.Collections;
using UnityEngine;

namespace LSWTest.Shop
{
    public class ShopItem
    {
        Item item;
        int availability;
        float price;
        int quantitiyInTransaction;

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

        public string GetAvailablity()
        {
            return availability.ToString();
        }

        public Sprite GetIcon()
        {
            return item.GetIcon();
        }

        public string GetPrice()
        {
            return price.ToString();
        }

        internal string GetQuantityInTransaction()
        {
            return quantitiyInTransaction.ToString();
        }
    }
}