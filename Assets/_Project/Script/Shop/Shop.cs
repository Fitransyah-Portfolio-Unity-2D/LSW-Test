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
    public class Shop : Shopable
    {
        public class ShopItem
        {
            Item item;
            int availability;
            float price;
            int quantitiyInTransaction;
        }

        public event Action onChange;

        public IEnumerable<Item> GetFilteredItems() { return null; }
        public void SelectFilter(ItemCategory category) { }
        public ItemCategory GetFilter() { return ItemCategory.None; }
        public void SelectMode(bool isBuying) { }
        public bool IsBuyingMode() { return true; }
        public bool CanTransact() { return true; }
        public void ConfirmTransaction() { }
        public float TransactionTotal() { return 0; }
        public void AddToTransaction(Item item, int quantity) { }

        public override void HandleCollisionTriggered(GameObject player)
        {
            player.GetComponent<Shopper>().SetActiveShop(this);
            player.GetComponent<PlayerMovement>().SetGameMode(GameMode.Shop);
        }
    }
}
