using LSWTest.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSWTest.Shop
{ 
    public class Shopper : MonoBehaviour
    {
        Shop activeShop = null;

        public event Action activeShopChange;

        public void SetActiveShop(Shop shop)
        {
            activeShop = shop;

            Debug.Log(shop.gameObject.name);

            if (activeShop != null)
            {
                activeShopChange();
            }
        }
        public Shop GetActiveShop()
        {
            return activeShop;
        }
        public void CloseShop()
        {
            activeShop = null;
            GetComponent<PlayerMovement>().SetGameMode(GameMode.Play);

            activeShopChange();
        }

    }
}
