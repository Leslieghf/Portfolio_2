using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Portfolio_2.Movement.FPS
{
    using Data;

    public class FPSMovement : MonoBehaviour
    {
        [SerializeField] private FPSMovementData data;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private CharacterController characterController;

        [HideInInspector] public bool canMove = true;

        private Vector3 moveDirection = Vector3.zero;
        private float rotationX = 0;

        void Update()
        {
            Vector3 worldForward = transform.TransformDirection(Vector3.forward);
            Vector3 worldRight = transform.TransformDirection(Vector3.right);
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float currentSpeedX = canMove ? (isRunning ? data.runningSpeed : data.walkingSpeed) * Input.GetAxis("Vertical") : 0.0f;
            float currentSpeedY = canMove ? (isRunning ? data.runningSpeed : data.walkingSpeed) * Input.GetAxis("Horizontal") : 0.0f;
            float movementDirectionY = moveDirection.y;
            moveDirection = (worldForward * currentSpeedX) + (worldRight * currentSpeedY);

            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                moveDirection.y = data.jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= data.gravity * Time.deltaTime;
            }

            characterController.Move(moveDirection * Time.deltaTime);

            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * data.lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -data.lookXLimit, data.lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * data.lookSpeed, 0);
            }
        }
    } 
}
