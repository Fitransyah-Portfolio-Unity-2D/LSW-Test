using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LSWTest.Inventory
{
    public class PurseUI : MonoBehaviour
    {
        [SerializeField] TMP_Text balanceField;

        Purse playerPurse = null;

        private void Start()
        {
            playerPurse = GameObject.FindGameObjectWithTag("Player").GetComponent<Purse>();

            if (playerPurse != null)
            {
                playerPurse.OnChange += RefreshUI;
            }    

            RefreshUI();
        }

        void RefreshUI()
        {
            balanceField.text = $"${playerPurse.GetBalance():N2}";
        }
    }

}