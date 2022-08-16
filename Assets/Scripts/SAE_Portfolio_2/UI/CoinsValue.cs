using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SAE_Portfolio_2.UI
{
    using SAE_Portfolio_2.Currency.Data;

    public sealed class CoinsValue : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private Coins coins;

        private void Awake()
        {
            coins.OnBalanceChanged.AddListener(() => { Refresh(); });
            Refresh();
        }

        public void Refresh()
        {
            text.text = $"Coins: {coins.GetBalance()}";
        }
    } 
}
