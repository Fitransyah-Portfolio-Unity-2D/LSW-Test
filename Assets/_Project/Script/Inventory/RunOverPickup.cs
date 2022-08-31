using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace LSWTest.Inventory
{
    [RequireComponent(typeof(Pickup))]
    public class RunOverPickup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (collision.gameObject == player)
            {
                GetComponent<Pickup>().PickupItem();
            }
        }
    }
}

