using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Portfolio_2.Movement.FPS.Data
{
    [CreateAssetMenu(fileName = "FPSMovementData", menuName = "Data/Movement/FPSMovementData", order = 1)]
    public class FPSMovementData : ScriptableObject
    {
        public float walkingSpeed;
        public float runningSpeed;
        public float jumpSpeed;
        public float gravity;
        public float lookSpeed;
        public float lookXLimit;
        public float bobbingSpeed;
        public float bobbingAmount;
    } 
}
