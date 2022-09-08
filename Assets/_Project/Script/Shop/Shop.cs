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
        Shopper currentShopper = null;

        public event Action onChange;

        public void SetShopper(Shopper shopper)
        {
            currentShopper = shopper;
        }

        public IEnumerable<ShopItem> GetFilteredItems() 
        {
            return GetAllItems();
        }
        public IEnumerable<ShopItem> GetAllItems()
        {
            foreach (StockItemConfig config in stockConfig)
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
        public void ConfirmTransaction() 
        { 
            // 1. Get the shopper
            PlayerInventory shopperInventory = currentShopper.GetComponent<PlayerInventory>();
            Purse shopperPurse = currentShopper.GetComponent<Purse>();
            if (shopperInventory == null || shopperPurse == null) return;

            // 2. transfer to or from inventory

           
            foreach (ShopItem shopItem in GetAllItems())
            {
                Item item = shopItem.GetInventoryItem();
                int quantity = shopItem.GetQuantityInTransaction();
                float price = shopItem.GetPrice();

                // handle stackable bug
                for (int i = 0; i < quantity; i++)
                {
                    if (shopperPurse.GetBalance() < price) break;
                    
                    bool succes = shopperInventory.AddToFirstEmptySlot(item, 1);

                    if (succes)
                    {
                        // 3. removal from transaction 
                        AddToTransaction(item, -1);

                        // 4. debit or credit funds
                        shopperPurse.UpdateBalance(-price);
                    }
                }
                
            }
            
            
            // debit or credit funds
        }
        public float TransactionTotal() 
        {
            float total = 0;
            foreach(ShopItem item in GetAllItems())
            {
                total += item.GetPrice() * item.GetQuantityInTransaction();
            }
            return total;
        }
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
