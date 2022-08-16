using System.Collections.Generic;
using UnityEngine;

namespace SAE_Portfolio_2.Data
{
    public sealed class GameData
    {
        public Vector3[] remainingCoins;
        public int coinBalance;
        public Vector3 playerPosition;

        public GameData(Vector3[] remainingCoins, int coinBalance, Vector3 playerPosition)
        {
            this.remainingCoins = remainingCoins;
            this.coinBalance = coinBalance;
            this.playerPosition = playerPosition;
        }
    } 
}
