using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SAE_Portfolio_2.Currency.Data
{
    [CreateAssetMenu(fileName = "Coins", menuName = "Data/Currency/Coins", order = 1)]
    public class Coins : ScriptableObject
    {
        [SerializeField] private int balance;
        [SerializeField] private UnityEvent onBalanceChanged;
        public UnityEvent OnBalanceChanged
        {
            get
            {
                return onBalanceChanged;
            }
        }

        public void SetBalance(int balance)
        {
            this.balance = balance;
            onBalanceChanged.Invoke();
        }

        public int GetBalance()
        {
            return balance;
        }

        public void AddBalance(int balance)
        {
            SetBalance(GetBalance() + balance);
        }
    } 
}
