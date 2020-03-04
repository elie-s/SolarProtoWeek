using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    public class CelestialBody : MonoBehaviour
    {
        [SerializeField] private float massReference = 1.0f;
        [SerializeField] private float massMultiplier = 1.0f;

        public float Mass => massReference * 4.0f / 3.0f * Mathf.PI * transform.localScale.x * massMultiplier;

        public Vector3 GravitationalForce(Vector3 _pos, float _mass, float _grav)
        {
            Vector3 direction = transform.position - _pos;
            Vector3 result = direction.normalized * (_grav * (Mass * _mass) / Mathf.Pow(direction.magnitude, 2));

            return result;
        }
    }
}