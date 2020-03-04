using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    public class OrbitMotion : MonoBehaviour
    {
        [SerializeField] private Transform origin;
        [SerializeField] private float speed = 5.0f;
        [SerializeField] private float speedMultiplier = 1.0f;

        void FixedUpdate()
        {
            Orbit();
        }

        private void Orbit()
        {
            transform.RotateAround(origin.position, Vector3.up, speedMultiplier * speed * Time.fixedDeltaTime / Mathf.Pow(Vector3.Distance(transform.position, origin.position), 2));
        }
    }
}