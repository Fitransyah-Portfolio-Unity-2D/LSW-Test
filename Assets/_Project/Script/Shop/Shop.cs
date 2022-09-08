using LSWTest.Core;
using LSWTest.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Item = LSWTest.Inventory.Item;

namespace LSWTest.Shop
{
    public class Shop : Interact
    {
        [SerializeField] string shopName;
        [Range(0,100)]
        [SerializeField] float sellingPercentage = 80f;

        // Stock config
        [SerializeField] StockItemConfig[] stockConfig;

        [Serializable]
        class StockItemConfig
        {
            public Item item;
            public int initialStock;
            [Range(0, 100)]
            public float buyingDiscountPercentage;
        }

        Dictionary<Item, int> transaction = new Dictionary<Item, int>();
        Dictionary<Item, int> stock = new Dictionary<Item, int>();  
        Shopper currentShopper = null;

        bool isBuyingMode = true;

        ItemCategory filter = ItemCategory.None;

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
            foreach(ShopItem shopItem in GetAllItems())
            {
                Item item = shopItem.GetInventoryItem();
                if (item.GetCategory() == filter || filter == ItemCategory.None)
                {
                    yield return shopItem;
                }
            }
        }
        public IEnumerable<ShopItem> GetAllItems()
        {
            foreach (StockItemConfig config in stockConfig)
            {
                float price = GetPrice(config);
                int quantityInTransaction = 0;
                transaction.TryGetValue(config.item, out quantityInTransaction);
                int availability = GetAvailability(config.item);
                yield return new ShopItem(config.item, availability, price, quantityInTransaction);
            }
        }


        public void SelectFilter(ItemCategory category) 
        {
            filter = category;
            print(category);

            if (onChange != null)
            {
                onChange();
            }
        }

        public ItemCategory GetFilter() 
        { 
            return filter; 
        }
        public void SelectMode(bool isBuying) 
        {
            isBuyingMode = isBuying;

            if (onChange != null)
            {
                onChange();
            }
        }
        public bool IsBuyingMode() 
        { 
            return isBuyingMode; 
        }
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

                if (isBuyingMode)
                {
                    BuyItem(shopperInventory, shopperPurse, item, quantity, price);
                }
                else
                {
                    SellItem(shopperInventory, shopperPurse, item, quantity, price);
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
            int availability = GetAvailability(item);
            if (transaction[item] + quantity > availability)
            {
                transaction[item] = availability;
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

        private float GetPrice(StockItemConfig config)
        {
            if (isBuyingMode)
            {
                return config.item.GetPrice() * (1 - config.buyingDiscountPercentage / 100);
            }
            return config.item.GetPrice() * (sellingPercentage / 100);
        }


        private int GetAvailability(Item item)
        {
            if (isBuyingMode)
            {
                return stock[item];
            }

            return CountItemsInInventory(item);
        }

        private int CountItemsInInventory(Item item)
        {
            PlayerInventory inventory = currentShopper.GetComponent<PlayerInventory>();
            if (inventory == null) return 0;

            int total = 0;
            for (int i = 0; i < inventory.GetSize(); i++)
            {
                if (inventory.GetItemInSlot(i) == item)
                {
                    total += inventory.GetNumberInSlot(i);
                }
            }
            return total;
        }


        private void BuyItem(PlayerInventory shopperInventory, Purse shopperPurse, Item item, int quantity, float price)
        {
            // handle stackable bug
            for (int i = 0; i < quantity; i++)
            {
                if (shopperPurse.GetBalance() < price) return;

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
        private void SellItem(PlayerInventory shopperInventory, Purse shopperPurse, Item item, int quantity, float price)
        {
            int slot = FindFirstItemSlot(shopperInventory, item);
            if (slot == -1) return;

            AddToTransaction(item, -1);
            shopperInventory.RemoveFromSlot(slot, 1);
            stock[item]++;
            shopperPurse.UpdateBalance(price);

        }
        private int FindFirstItemSlot(PlayerInventory shopperInventory, Item item)
        {
            for (int i = 0; i < shopperInventory.GetSize(); i++)
            {
                if (shopperInventory.GetItemInSlot(i) == item)
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
