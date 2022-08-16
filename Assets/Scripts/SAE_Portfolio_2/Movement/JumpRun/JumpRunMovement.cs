using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Portfolio_2.Movement.JumpRun
{
    using Data;

    public class JumpRunMovement : MonoBehaviour
    {
        [SerializeField] private JumpRunMovementData data;
        [SerializeField] private LayerMask groundCheckLayerMask;
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private new BoxCollider2D collider;
        private bool hasDoubleJumped;

        void Update()
        {
            if (IsGrounded())
            {
                hasDoubleJumped = false;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
            }
            else if (!hasDoubleJumped)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    hasDoubleJumped = true;
                    Jump(); 
                }
            }
        }

        private void FixedUpdate()
        {
            if (Mathf.Abs(rigidbody.velocity.x) <= data.Speed)
            {
                rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * data.Speed, rigidbody.velocity.y);
            }
        }

        private void Jump()
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0.0f);
            rigidbody.AddForce(Vector2.up * data.JumpForce, ForceMode2D.Impulse);
        }

        private bool IsGrounded()
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center, Vector2.down, collider.bounds.extents.y + 0.1f, groundCheckLayerMask);
            return raycastHit.collider != null;
        }
    } 
}
