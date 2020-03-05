using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarProto
{
    public class CelestialBody : MonoBehaviour
    {
        [SerializeField] private float massReference = 1.0f;
        [SerializeField] private float massMultiplier = 1.0f;
        [SerializeField] private float maxDistance = 0.0f;

        public float Mass => massReference * 4.0f / 3.0f * Mathf.PI * transform.localScale.x * massMultiplier;

        public Vector3 GravitationalForce(Vector3 _pos, float _mass, float _grav)
        {
            if (maxDistance > 0.0f && Vector3.Distance(_pos, transform.position) > maxDistance) return Vector3.zero;

            Vector3 direction = transform.position - _pos;
            Vector3 result = direction.normalized * (_grav * (Mass * _mass) / Mathf.Pow(direction.magnitude, 2));

            return result;
        }

        public void SetValues(float _massRef, float _massMultiplier, float _maxDistance)
        {
            massReference = _massRef;
            massMultiplier = _massMultiplier;
            maxDistance = _maxDistance;
        }

        public float[] GetValues()
        {
            return new float[3] { massReference, massMultiplier, maxDistance };
        }
    }
}