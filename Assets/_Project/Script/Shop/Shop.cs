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
        
        public event Action onChange;

        public IEnumerable<ShopItem> GetFilteredItems() 
        {
            yield return new ShopItem(Item.GetFromID("ceb7ef13-d4c4-4af0-8c8a-30fc8b65d6e1"), 10, 25.976780f, 0);
            yield return new ShopItem(Item.GetFromID("4f89c0f2-762b-454e-8dc2-d2ae2684144f"), 10, 19.4555640f, 0);
            yield return new ShopItem(Item.GetFromID("0868566c-009f-487f-896c-aa50b8691d43"), 10, 34.55555f, 0);
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
        public void AddToTransaction(Item item, int quantity) { }

        public override void HandleCollisionTriggered(GameObject player)
        {
            player.GetComponent<Shopper>().SetActiveShop(this);
        }
    }
}
