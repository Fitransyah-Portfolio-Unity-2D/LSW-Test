using LSWTest.Core;
using LSWTest.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Item = LSWTest.Inventory.Item;

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
        Dictionary<Item, int> stock = new Dictionary<Item, int>();  
        Shopper currentShopper = null;

        public event Action onChange;

        private void Awake()
        {
            foreach(StockItemConfig config in stockConfig)
            {
                stock[config.item] = config.initialStock;
            }
        }

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
                int currentStock = stock[config.item];
                yield return new ShopItem(config.item, currentStock, price, quantityInTransaction);
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

                        // stock deduction
                        stock[item]--;

                        // 4. debit or credit funds
                        shopperPurse.UpdateBalance(-price);
                    }
                }
                
            }

            if (onChange != null)
            {
                onChange();
            }
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

            // stock check
            if (transaction[item] + quantity > stock[item])
            {
                transaction[item] = stock[item];
            }
            else
            {
                transaction[item] += quantity;
            }
            

            // handle remove quantity button if its zero
            if (transaction[item] <= 0)
            {
                transaction.Remove(item);
            }

            if (onChange != null)
            {
                onChange();
            }
        }

        public override void HandleCollisionTriggered(GameObject player)
        {
            player.GetComponent<Shopper>().SetActiveShop(this);
        }
    }
}
