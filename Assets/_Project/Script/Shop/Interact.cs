using System.Collections;
using UnityEngine;

namespace LSWTest.Shop
{
    public abstract class Interact : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var player = GameObject.FindWithTag("Player");
            if (collision.gameObject == player)
            {
                HandleCollisionTriggered(player);
            }
        }
        public abstract void HandleCollisionTriggered(GameObject player);

    }
}