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
            if (activeShop != null)
            {
                activeShop.SetShopper(null);
            }
            
            activeShop = shop;

            if (activeShop != null)
            {
                activeShop.SetShopper(this);
            }

            if (activeShop != null)
            {
                activeShopChange();
                GetComponent<PlayerMovement>().SetGameMode(GameMode.Shop);
            }
        }
        public Shop GetActiveShop()
        {
            return activeShop;
        }
        public void RemoveActiveShop()
        {
            activeShop = null;
            GetComponent<PlayerMovement>().SetGameMode(GameMode.Play);
            activeShopChange();
        }

    }
}
