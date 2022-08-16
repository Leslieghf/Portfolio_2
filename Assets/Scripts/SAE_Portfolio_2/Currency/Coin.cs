using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Portfolio_2.Currency
{
    using Data;

    public sealed class Coin : MonoBehaviour
    {
        [SerializeField] private Coins coins;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                coins.AddBalance(1);
                Destroy(gameObject);
            }
        }
    } 
}
