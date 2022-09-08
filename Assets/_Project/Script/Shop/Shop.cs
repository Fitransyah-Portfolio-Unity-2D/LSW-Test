using LSWTest.Core;
using LSWTest.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace LSWTest.Shop
{
    public class Shop : Interact
    {
        [SerializeField] string shopName;

        // Stock config
        [SerializeField]
        StockItemConfig[] stockConfig;

        [Serializable]
        class StockItemConfig
        {
            public Item item;
            public int initialStock;
            public float buyingDiscountPercentage;
        }

        Dictionary<Item, int> transaction = new Dictionary<Item, int>(); 

        public event Action onChange;

        public IEnumerable<ShopItem> GetFilteredItems() 
        {
            foreach(StockItemConfig config in stockConfig)
            {
                float price = config.item.GetPrice() * (1 - config.buyingDiscountPercentage / 100);
                int quantityInTransaction = 0;
                transaction.TryGetValue(config.item, out quantityInTransaction);
                yield return new ShopItem(config.item, config.initialStock, price, quantityInTransaction);
            }
        }
        public void SelectFilter(ItemCategory category) { }
        public ItemCategory GetFilter() { return ItemCategory.None; }
        public void SelectMode(bool isBuying) { }
        public bool IsBuyingMode() { return true; }
        public bool CanTransact() { return true; }
        public void ConfirmTransaction() { }
        public float TransactionTotal() { return 0; }
        public string GetShopName()
        {
            return shopName;
        }
        public void AddToTransaction(Item item, int quantity) 
        {
            if (!transaction.ContainsKey(item))
            {
                transaction[item] = 0;
            }

            transaction[item] += quantity;

            // handle remove quantity button if its zero
            if (transaction[item] <= 0)
            {
                transaction.Remove(item);
            }

            if (onChange != null)
            {
                onChange(); //
            }
        }

        public override void HandleCollisionTriggered(GameObject player)
        {
            player.GetComponent<Shopper>().SetActiveShop(this);
        }
    }
}
