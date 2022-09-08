using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSWTest.Inventory
{
    public class Purse : MonoBehaviour
    {
        [SerializeField] float startingBalance = 400f;

        [SerializeField] float balance = 0;

        public event Action OnChange;

        private void Awake()
        {
            balance = startingBalance;
        }

        public float GetBalance()
        {
            return balance;
        }

        public void UpdateBalance(float amount)
        {
            balance += amount;
            
            if (OnChange != null)
            {
                OnChange();
            }
        }
    }
}
