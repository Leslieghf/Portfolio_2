using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Portfolio_2.GravityGun
{
    //Because I'm uncreative I implemented (and adapted) a gravity gun script that I found at https://pastebin.com/w1G8m3dH
    public class GravityGun : MonoBehaviour
    {
        private Rigidbody heldRigidbody;

        #region Held Object Info
        private Vector3 hitOffsetLocal;
        private float currentGrabDistance;
        private RigidbodyInterpolation initialInterpolationSetting;
        private Vector3 rotationDifferenceEuler;
        #endregion

        private Vector2 rotationInput;
        private const float maxGrabDistance = 30;
        private Ray CenterRay()
        {
            return Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        }

        void Update()
        {
            if (!Input.GetMouseButton(0))
            {
                if (heldRigidbody != null)
                {
                    heldRigidbody.interpolation = initialInterpolationSetting;
                    heldRigidbody = null;
                }

                return;
            }

            if (heldRigidbody == null)
            {
                Ray ray = CenterRay();
                RaycastHit hit;

                Debug.DrawRay(ray.origin, ray.direction * maxGrabDistance, Color.blue, 0.01f);

                if (Physics.Raycast(ray, out hit, maxGrabDistance))
                {
                    if (hit.rigidbody != null && !hit.rigidbody.isKinematic)
                    {
                        heldRigidbody = hit.rigidbody;
                        initialInterpolationSetting = heldRigidbody.interpolation;
                        rotationDifferenceEuler = hit.transform.rotation.eulerAngles - transform.rotation.eulerAngles;

                        hitOffsetLocal = hit.transform.InverseTransformVector(hit.point - hit.transform.position);

                        currentGrabDistance = Vector3.Distance(ray.origin, hit.point);

                        heldRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (heldRigidbody)
            {
                Ray ray = CenterRay();

                heldRigidbody.MoveRotation(Quaternion.Euler(rotationDifferenceEuler + transform.rotation.eulerAngles));

                Vector3 holdPoint = ray.GetPoint(currentGrabDistance);
                Debug.DrawLine(ray.origin, holdPoint, Color.blue, Time.fixedDeltaTime);

                Vector3 currentEuler = heldRigidbody.rotation.eulerAngles;
                heldRigidbody.transform.RotateAround(holdPoint, transform.right, rotationInput.y);
                heldRigidbody.transform.RotateAround(holdPoint, transform.up, -rotationInput.x);

                heldRigidbody.angularVelocity = Vector3.zero;
                rotationInput = Vector2.zero;
                rotationDifferenceEuler = heldRigidbody.transform.rotation.eulerAngles - transform.rotation.eulerAngles;

                Vector3 centerDestination = holdPoint - heldRigidbody.transform.TransformVector(hitOffsetLocal);

                Vector3 toDestination = centerDestination - heldRigidbody.transform.position;

                Vector3 force = toDestination / Time.fixedDeltaTime;

                heldRigidbody.velocity = Vector3.zero;
                heldRigidbody.AddForce(force, ForceMode.VelocityChange);
            }
        }
    } 
}