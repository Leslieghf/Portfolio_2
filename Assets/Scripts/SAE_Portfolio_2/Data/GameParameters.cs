using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Portfolio_2.Data
{
    [CreateAssetMenu(fileName = "GameParameters", menuName = "Data/GameParameters", order = 1)]
    public sealed class GameParameters : ScriptableObject
    {
        public bool IsNewGame;
    } 
}
