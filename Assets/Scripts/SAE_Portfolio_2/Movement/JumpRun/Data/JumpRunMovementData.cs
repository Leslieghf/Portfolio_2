using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Portfolio_2.Movement.JumpRun.Data
{
    [CreateAssetMenu(fileName = "JumpRunMovementData", menuName = "Data/Movement/JumpRunMovementData", order = 1)]
    public class JumpRunMovementData : ScriptableObject
    {
        public float Speed;
        public float JumpForce;
        public bool CanDoubleJump;
        public float CoyoteTime;
    } 
}
